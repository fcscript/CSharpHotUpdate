using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Threading;

public interface ISerializable
{
    void Serialize(CSerialize ar);
}

public enum SerializeType
{
    read,
    write,
    append,
};

class CSerialzieStream : Stream
{
    byte[] m_pData;
    int m_nSize;
    int m_nSizeMax;
    int m_nPos;
    SerializeType m_arType;
    bool m_bLittleByte;
    public CSerialzieStream(SerializeType arType)
    {
        ResetStream(arType);
    }
    public CSerialzieStream(SerializeType arType, byte[] buffer, int nBufSize)
    {
        ResetStream(arType, buffer, nBufSize);
    }
    public void ResetStream(SerializeType arType)
    {
        m_nSize = 0;
        m_nPos = 0;
        m_arType = arType;
        m_bLittleByte = System.BitConverter.IsLittleEndian;
    }
    public void ResetStream(SerializeType arType, byte[] buffer, int nBufSize)
    {
        // 如果是只读，就不用拷贝了
        if(arType == SerializeType.read)
        {
            m_pData = buffer;
            m_nSize = m_nSizeMax = nBufSize;
        }
        else
        {
            m_pData = new byte[nBufSize];
            if (buffer != null)
                Array.Copy(buffer, m_pData, nBufSize);
            m_nSize = m_nSizeMax = nBufSize;
        }        
        m_nPos = 0;
        m_arType = arType;
        m_bLittleByte = System.BitConverter.IsLittleEndian;
    }
    void reserve(int nSizeMax)
    {
        if (m_nSizeMax < nSizeMax)
        {
            byte[] pData = new byte[nSizeMax];
            m_nSizeMax = nSizeMax;
            if (m_nSize > 0)
                Array.Copy(m_pData, pData, m_nSize);
            m_pData = pData;
        }
    }
    void auto_grow(int nGrowSize)
    {
        if (nGrowSize > 0 && m_nPos + nGrowSize > m_nSizeMax)
        {
            int nNewSize = m_nSizeMax * 2 + (nGrowSize + 4095) / 4096 * 4096;
            if (nNewSize < 4096)
                nNewSize = 4096;
            reserve(nNewSize);
        }
    }
    // 功能：交换当前INT变量
    // 参数：nIntSize - 整数的字节数
    void swap_int(int nIntSize, int nPos)
    {
        byte yTemp = 0;
        switch (nIntSize)
        {
            case 2:  // [1][2] ==> [2][1]
                yTemp = m_pData[nPos]; m_pData[nPos] = m_pData[nPos + 1]; m_pData[nPos + 1] = yTemp;
                break;
            case 4:  // [1][2][3][4] ==> [4][3][2][1]
                yTemp = m_pData[nPos]; m_pData[nPos] = m_pData[nPos + 3]; m_pData[nPos + 3] = yTemp;
                yTemp = m_pData[nPos + 1]; m_pData[nPos + 1] = m_pData[nPos + 2]; m_pData[nPos + 2] = yTemp;
                break;
            case 8:  // [1][2][3][4][5][6][7][8] ==> [8][7][6][5][4][3][2][1]
                yTemp = m_pData[nPos]; m_pData[nPos] = m_pData[nPos + 7]; m_pData[nPos + 7] = yTemp;
                yTemp = m_pData[nPos + 1]; m_pData[nPos + 1] = m_pData[nPos + 6]; m_pData[nPos + 6] = yTemp;
                yTemp = m_pData[nPos + 2]; m_pData[nPos + 2] = m_pData[nPos + 5]; m_pData[nPos + 5] = yTemp;
                yTemp = m_pData[nPos + 3]; m_pData[nPos + 3] = m_pData[nPos + 4]; m_pData[nPos + 4] = yTemp;
                break;
            default:
                break;
        }
    }
    // 功能：自动变换大小端整数（跨平台使用)
    void AutoSwapInt(int nIntSize, int nPos)
    {
        if (!m_bLittleByte)
        {
            swap_int(nIntSize, nPos);
        }
    }

    public byte[] GetBuffer()
    {
        return m_pData;
    }
    public int GetBufferSize()
    {
        return m_nPos;
    }
    public override bool CanRead { get { return m_arType == SerializeType.read; } }
    public override bool CanSeek { get { return true; } }
    public override bool CanWrite { get { return m_arType == SerializeType.read || m_arType == SerializeType.append; } }
    public override long Length { get { return m_nSize; } }
    public override long Position
    {
        get { return m_nPos; }
        set
        {
            Seek(value, SeekOrigin.Begin);
        }
    }
    public override void Flush()
    {
        // 写入文件
        m_nPos = 0;
        m_nSize = 0;
    }
    public override int Read(byte[] buffer, int offset, int count)
    {
        if (m_nPos + count > m_nSize)
        {
            count = m_nSize - m_nPos;
        }
        if (count > 0)
        {
            //Array.Copy(m_pData, m_nPos, buffer, offset, count);
            CopyArray(m_pData, m_nPos, ref buffer, offset, count);
            m_nPos += count;
        }
        return count;
    }
    public override long Seek(long offset, SeekOrigin origin)
    {
        switch (origin)
        {
            case SeekOrigin.Begin:
                m_nPos = (int)offset;
                break;
            case SeekOrigin.Current:
                m_nPos += (int)offset;
                break;
            case SeekOrigin.End:
                m_nPos = m_nSize - (int)offset;
                break;
            default:
                break;
        }
        if (m_nPos < 0)
            m_nPos = 0;
        else if (m_nPos > m_nSizeMax)
            m_nPos = m_nSizeMax;
        if (m_arType == SerializeType.write
            || m_arType == SerializeType.append)
        {
            if (m_nSize < m_nPos)
                m_nSize = m_nPos;
        }
        return m_nPos;
    }
    public override void SetLength(long value)
    {
        m_nSize = (int)value;
        if (m_nSize < 0)
            m_nSize = 0;
        if (m_nSize > m_nSizeMax)
            m_nSize = m_nSizeMax;
        if (m_nPos > m_nSize)
            m_nPos = m_nSize;
    }
    public static void CopyArray(byte[] src, int nSrcOffset, ref byte[] des, int nDesOffset, int count)
    {
        for (int i = 0; i < count; ++i)
        {
            des[i + nDesOffset] = src[i + nSrcOffset];
        }
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        auto_grow(count);
        if (count > 0)
        {
            CopyArray(buffer, offset, ref m_pData, m_nPos, count);
            m_nPos += count;
            if (m_nSize < m_nPos)
                m_nSize = m_nPos;
        }
    }
    public void ReadInt(ref int nValue)
    {
        if (m_nPos + 4 <= m_nSizeMax)
        {
            nValue = System.BitConverter.ToInt32(m_pData, m_nPos);
            m_nPos += 4;
        }
    }
    public void ReadInt(ref uint nValue)
    {
        if (m_nPos + 4 <= m_nSizeMax)
        {
            nValue = System.BitConverter.ToUInt32(m_pData, m_nPos);
            m_nPos += 4;
        }
    }
    public void ReadInt(ref long nValue)
    {
        if (m_nPos + 8 <= m_nSizeMax)
        {
            nValue = System.BitConverter.ToInt64(m_pData, m_nPos);
            m_nPos += 8;
        }
    }
    public void ReadInt(ref ulong nValue)
    {
        if (m_nPos + 8 <= m_nSizeMax)
        {
            nValue = System.BitConverter.ToUInt64(m_pData, m_nPos);
            m_nPos += 8;
        }
    }
    public void ReadInt(ref byte yValue)
    {
        if (m_nPos + 1 <= m_nSizeMax)
        {
            yValue = m_pData[m_nPos++];
        }
    }
    public void ReadInt(ref short wValue)
    {
        if (m_nPos + 2 <= m_nSizeMax)
        {
            wValue = System.BitConverter.ToInt16(m_pData, m_nPos);
            m_nPos += 2;
        }
    }
	public void ReadInt(ref ushort wValue)
    {
        if (m_nPos + 2 <= m_nSizeMax)
        {
            wValue = System.BitConverter.ToUInt16(m_pData, m_nPos);
            m_nPos += 2;
        }
    }
    public void ReadInt(ref bool bValue)
    {
        if (m_nPos + 1 <= m_nSizeMax)
        {
            bValue = System.BitConverter.ToBoolean(m_pData, m_nPos);
            m_nPos++;
        }
    }
    public void ReadInt(ref float fValue)
    {
        if (m_nPos + 4 <= m_nSizeMax)
        {
            fValue = System.BitConverter.ToSingle(m_pData, m_nPos);
            m_nPos += 4;
        }
    }
    public void ReadInt(ref double fValue)
    {
        if (m_nPos + 8 <= m_nSizeMax)
        {
            fValue = System.BitConverter.ToDouble(m_pData, m_nPos);
            m_nPos += 8;
        }
    }
    public void ReadInt(ref Color32 color)
    {
        // a, r, g, b
        if(m_nPos + 4 <= m_nSizeMax)
        {
            color.a = m_pData[m_nPos++];
            color.r = m_pData[m_nPos++];
            color.g = m_pData[m_nPos++];
            color.b = m_pData[m_nPos++];
        }
    }
    public void ReadString(ref string str)
    {
        int nLen = 0;
        ReadInt(ref nLen);
        if (nLen >= 0 && m_nPos + nLen <= m_nSizeMax)
        {
            if (nLen > 0)
            {
                str = System.Text.Encoding.UTF8.GetString(m_pData, m_nPos, nLen);
                m_nPos += nLen;
            }
            else
            {
                str = "";
            }
        }
        else
        {
            str = string.Empty;
        }
    }
    public void ReadStringUTF8(ref string str, int nLen)
    {
        if (nLen >= 0 && m_nPos + nLen <= m_nSizeMax)
        {
            if (nLen > 0)
            {
                int nI = m_nPos;
                int nEnd = m_nPos + nLen;
                int nRealLen = 0;
                for (; nI < nEnd; ++nI)
                {
                    if (m_pData[nI] == 0)
                        break;
                    ++nRealLen;
                }
                str = System.Text.Encoding.UTF8.GetString(m_pData, m_nPos, nLen);
            }
            else
            {
                str = "";
            }
        }
        else
            str = string.Empty;
        m_nPos += nLen;
    }
    public void ReadStringUTF32(ref string str, int nLen)
    {
        nLen *= 4;
        if (nLen >= 0 && m_nPos + nLen <= m_nSizeMax)
        {
            if (nLen > 0)
            {
                // 只取有效的字符
                int nRealLen = 0;
                int nI = m_nPos;
                for (; nRealLen < nLen;)
                {
                    if (m_pData[nI] == 0
                        && m_pData[nI + 1] == 0
                        && m_pData[nI + 2] == 0
                        && m_pData[nI + 3] == 0)
                    {
                        break;
                    }
                    nI += 4;
                    nRealLen += 4;
                }
                str = System.Text.Encoding.UTF32.GetString(m_pData, m_nPos, nRealLen);
            }
            else
            {
                str = "";
            }
        }
        else
            str = string.Empty;
        m_nPos += nLen;
    }
    // 功能：读取整数数组
    public void ReadIntArray(ref byte[] vArray)
    {
        int nLen = 0;
        ReadInt(ref nLen);
        if (nLen > 0 && m_nPos + nLen <= m_nSizeMax)
        {
            vArray = new byte[nLen];
            for (int i = 0; i < nLen; ++i)
            {
                vArray[i] = m_pData[m_nPos++];
            }
        }
        else
        {
            vArray = null;
        }
    }
    // 功能：读取整数数组
    public void ReadIntArray(ref int[] vArray)
    {
        int nLen = 0;
        ReadInt(ref nLen);
        if (nLen > 0 && m_nPos + nLen * 4 <= m_nSizeMax)
        {
            vArray = new int[nLen];
            for (int i = 0; i < nLen; ++i)
            {
                vArray[i] = 0;
                ReadInt(ref vArray[i]);
            }
        }
        else
        {
            vArray = null;
        }
    }
    public void ReadIntArray(ref uint[] vArray)
    {
        int nLen = 0;
        ReadInt(ref nLen);
        if (nLen > 0 && m_nPos + nLen * 4 <= m_nSizeMax)
        {
            vArray = new uint[nLen];
            for (int i = 0; i < nLen; ++i)
            {
                vArray[i] = 0;
                ReadInt(ref vArray[i]);
            }
        }
        else
        {
            vArray = null;
        }
    }
    // 功能：读取浮点数组
    public void ReadIntArray(ref float[] vArray)
    {
        int nLen = 0;
        ReadInt(ref nLen);
        if (nLen > 0 && m_nPos + nLen * 4 <= m_nSizeMax)
        {
            vArray = new float[nLen];
            for (int i = 0; i < nLen; ++i)
            {
                ReadInt(ref vArray[i]);
            }
        }
        else
        {
            vArray = null;
        }
    }
    // 功能：读取字符数组
    public void ReadIntArray(ref string[] vArray)
    {
        int nLen = 0;
        ReadInt(ref nLen);
        if (nLen > 0 && nLen * 4 <= m_nSizeMax)
        {
            vArray = new string[nLen];
            for (int i = 0; i < nLen; ++i)
            {
                ReadString(ref vArray[i]);
            }
        }
        else
        {
            vArray = null;
        }
    }

    public void ReadIntArray(ref Vector3[] vArray)
    {
        int nLen = 0;
        ReadInt(ref nLen);
        if (nLen > 0 && nLen * 12 <= m_nSizeMax)
        {
            vArray = new Vector3[nLen];
            for (int i = 0; i < nLen; ++i)
            {
                ReadInt(ref vArray[i].x);
                ReadInt(ref vArray[i].y);
                ReadInt(ref vArray[i].z);
            }
        }
        else
        {
            vArray = null;
        }
    }
    public void ReadIntArray(ref Color32[] vArray)
    {
        int nLen = 0;
        ReadInt(ref nLen);
        if (nLen > 0 && nLen * 4 + m_nPos <= m_nSizeMax)
        {
            vArray = new Color32[nLen];
            // a, r, g, b
            int nReadLen = nLen * 4;
            if (nReadLen > m_nSizeMax - m_nPos)
                nReadLen = m_nSizeMax - m_nPos;
            nReadLen /= 4;
            for (int i = 0; i < nReadLen; ++i)
            {
                vArray[i].a = m_pData[m_nPos++];
                vArray[i].r = m_pData[m_nPos++];
                vArray[i].g = m_pData[m_nPos++];
                vArray[i].b = m_pData[m_nPos++];
            }
        }
        else
        {
            vArray = null;
        }
    }

    public void ReadArray(ref byte []vArray, int nLen)
    {
        if (vArray == null)
            vArray = new byte[nLen];
        for (int i = 0; i< nLen && m_nPos < m_nSizeMax; ++i)
        {
            vArray[i] = m_pData[m_nPos++];
        }
    }

    public void WriteInt(int nValue)
    {
        byte[] byBuf = System.BitConverter.GetBytes(nValue);
        Write(byBuf, 0, byBuf.Length);
    }
    public void WriteInt(uint nValue)
    {
        byte[] byBuf = System.BitConverter.GetBytes(nValue);
        Write(byBuf, 0, byBuf.Length);
    }
    public void WriteInt(long nValue)
    {
        byte[] byBuf = System.BitConverter.GetBytes(nValue);
        Write(byBuf, 0, byBuf.Length);
    }
    public void WriteInt(ulong nValue)
    {
        byte[] byBuf = System.BitConverter.GetBytes(nValue);
        Write(byBuf, 0, byBuf.Length);
    }
    public void WriteInt(byte yValue)
    {
        auto_grow(1);
        m_pData[m_nPos++] = yValue;
        if (m_nSize < m_nPos)
            m_nSize = m_nPos;
    }
    public void WriteInt(short wValue)
    {
        byte[] byBuf = System.BitConverter.GetBytes(wValue);
        Write(byBuf, 0, byBuf.Length);
    }
    public void WriteInt(ushort wValue)
    {
        byte[] byBuf = System.BitConverter.GetBytes(wValue);
        Write(byBuf, 0, byBuf.Length);
    }
    public void WriteInt(bool bValue)
    {
        byte[] byBuf = System.BitConverter.GetBytes(bValue);
        Write(byBuf, 0, byBuf.Length);
    }
    public void WriteInt(float fValue)
    {
        byte[] byBuf = System.BitConverter.GetBytes(fValue);
        Write(byBuf, 0, byBuf.Length);
    }
    public void WriteInt(double fValue)
    {
        byte[] byBuf = System.BitConverter.GetBytes(fValue);
        Write(byBuf, 0, byBuf.Length);
    }
    public void WriteInt(Color32 color)
    {
        byte[] byBuf = { color.a, color.r, color.g, color.b };
        Write(byBuf, 0, byBuf.Length);
    }
    public void WriteString(string str)
    {
        int nLen = 0;
        if (!string.IsNullOrEmpty(str))
        {
            byte[] byBuf = System.Text.Encoding.UTF8.GetBytes(str);
            nLen = byBuf.Length;
            WriteInt(nLen);
            auto_grow(nLen);
            Array.Copy(byBuf, 0, m_pData, m_nPos, nLen);
            m_nPos += nLen;
            if (m_nSize < m_nPos)
                m_nSize = m_nPos;
        }
        else
        {
            WriteInt(nLen);
        }
    }
    public void PushTextString(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            byte[] byBuf = System.Text.Encoding.UTF8.GetBytes(str);
            int nLen = byBuf.Length;
            auto_grow(nLen);
            Array.Copy(byBuf, 0, m_pData, m_nPos, nLen);
            m_nPos += nLen;
            if (m_nSize < m_nPos)
                m_nSize = m_nPos;
        }
    }
    public void PushTextString(byte[] str)
    {
        int nLen = str != null ? str.Length : 0;
        if (nLen > 0)
        {
            auto_grow(nLen);
            Array.Copy(str, 0, m_pData, m_nPos, nLen);
            //for (int i = 0; i < nLen; ++i)
            //{
            //    m_pData[i + m_nPos] = (byte)(str[i]);
            //}
            m_nPos += nLen;
            if (m_nSize < m_nPos)
                m_nSize = m_nPos;
        }
    }
    public void WriteStringUTF8(string str, int nSize)
    {
        auto_grow(nSize);
        int nCurWriteSize = 0;
        if (!string.IsNullOrEmpty(str))
        {
            byte[] byBuf = System.Text.Encoding.UTF8.GetBytes(str);
            nCurWriteSize = byBuf.Length;
            if (nCurWriteSize > nSize)
                nCurWriteSize = nSize;
            Array.Copy(byBuf, 0, m_pData, m_nPos, nCurWriteSize);
        }
        m_nPos += nSize;
        if (m_nSize < m_nPos)
            m_nSize = m_nPos;
    }
    public void WriteStringUTF32(string str, int nSize)
    {
        nSize *= 4;
        auto_grow(nSize);
        int nCurWriteSize = 0;
        if (!string.IsNullOrEmpty(str))
        {
            byte[] byBuf = System.Text.Encoding.UTF32.GetBytes(str);
            nCurWriteSize = byBuf.Length;
            if (nCurWriteSize > nSize)
                nCurWriteSize = nSize;
            Array.Copy(byBuf, 0, m_pData, m_nPos, nCurWriteSize);
        }
        m_nPos += nSize;
        if (m_nSize < m_nPos)
            m_nSize = m_nPos;
    }
    public void WriteIntArray(byte[] vArray)
    {
        int nLen = vArray != null ? vArray.Length : 0;
        WriteInt(nLen);
        if (nLen > 0)
        {
            auto_grow(nLen);
            for (int i = 0; i < nLen; ++i)
            {
                m_pData[i + m_nPos] = (byte)(vArray[i]);
            }
            m_nPos += nLen;
            if (m_nSize < m_nPos)
                m_nSize = m_nPos;
        }
    }
    // 功能：写入整数数组
    public void WriteIntArray(int[] vArray)
    {
        int nLen = vArray != null ? vArray.Length : 0;
        WriteInt(nLen);
        if (nLen > 0)
        {
            for (int i = 0; i < nLen; ++i)
            {
                WriteInt(vArray[i]);
            }
        }
    }
    // 功能：写入整数数组
    public void WriteIntArray(uint[] vArray)
    {
        int nLen = vArray != null ? vArray.Length : 0;
        WriteInt(nLen);
        if (nLen > 0)
        {
            for (int i = 0; i < nLen; ++i)
            {
                WriteInt(vArray[i]);
            }
        }
    }
    // 功能：写入浮点数组
    public void WriteIntArray(float[] vArray)
    {
        int nLen = vArray != null ? vArray.Length : 0;
        WriteInt(nLen);
        if (nLen > 0)
        {
            for (int i = 0; i < nLen; ++i)
            {
                WriteInt(vArray[i]);
            }
        }
    }
    public void WriteIntArray(Vector3[] vArray)
    {
        int nLen = vArray != null ? vArray.Length : 0;
        WriteInt(nLen);
        if (nLen > 0)
        {
            for (int i = 0; i < nLen; ++i)
            {
                WriteInt(vArray[i].x);
                WriteInt(vArray[i].y);
                WriteInt(vArray[i].z);
            }
        }
    }
    // 功能：写入字符串数组
    public void WriteIntArray(string[] vArray)
    {
        int nLen = vArray != null ? vArray.Length : 0;
        WriteInt(nLen);
        if (nLen > 0)
        {
            for (int i = 0; i < nLen; ++i)
            {
                WriteString(vArray[i]);
            }
        }
    }
    // 功能：写入颜色数组
    public void WriteIntArray(Color32 []vArray)
    {
        int nLen = vArray != null ? vArray.Length : 0;
        WriteInt(nLen);
        if (nLen > 0)
        {
            auto_grow(nLen * 4);
            for (int i = 0; i < nLen; ++i)
            {
                // a, r, g, b
                m_pData[m_nPos++] = vArray[i].a;
                m_pData[m_nPos++] = vArray[i].r;
                m_pData[m_nPos++] = vArray[i].g;
                m_pData[m_nPos++] = vArray[i].b;
            }
            if (m_nSize < m_nPos)
                m_nSize = m_nPos;
        }
    }

    public void WriteArray(byte[] vArray, int nLen)
    {
        auto_grow(nLen);
        int nRealSize = vArray != null ? vArray.Length : 0;
        if (nRealSize > nLen)
            nRealSize = nLen;
        for (int i = 0; i < nRealSize && m_nPos < m_nSizeMax; ++i)
        {
            m_pData[m_nPos++] = vArray[i];
        }
        m_nPos += nLen - nRealSize;
        if (m_nSize < m_nPos)
            m_nSize = m_nPos;
    }
};

// 一个内存序列化的类
public class CSerialize
{
    CSerialzieStream m_arFile;
    SerializeType m_arType;
    string m_szFileName;
    int m_nVersion;

    bool m_bCreate = false;

    public CSerialize(SerializeType arType)
    {
        m_arType = arType;
        m_arFile = new CSerialzieStream(arType);
        m_nVersion = 0;
    }
    public CSerialize(SerializeType arType, byte[] buffer, int nBufSize)
    {
        m_arType = arType;
        m_arFile = new CSerialzieStream(arType, buffer, nBufSize);
        m_nVersion = 0;
    }
    public CSerialize(SerializeType arType, string szFileName)
    {
        m_arType = arType;
        m_szFileName = szFileName;
        m_nVersion = 0;
        if (m_arType == SerializeType.read)
        {
            if (System.IO.File.Exists(m_szFileName))
            {
                byte[] fileData = System.IO.File.ReadAllBytes(m_szFileName);
                m_arFile = new CSerialzieStream(arType, fileData, fileData.Length);
            }
            else
                m_arFile = new CSerialzieStream(arType, null, 0);
        }
        else
        {
            m_arFile = new CSerialzieStream(arType);
        }
    }
    // 功能：重置流
    public void ResetStream(SerializeType arType)
    {
        m_arType = arType;
        if(m_arFile == null)
            m_arFile = new CSerialzieStream(arType);
        m_arFile.ResetStream(arType);
        m_nVersion = 0;
    }
    public void ResetStream(SerializeType arType, byte[] buffer, int nBufSize)
    {
        m_arType = arType;
        if (m_arFile == null)
            m_arFile = new CSerialzieStream(arType, buffer, nBufSize);
        else
            m_arFile.ResetStream(arType, buffer, nBufSize);
        m_nVersion = 0;
    }
    static CSerialize s_readAr;
    public static CSerialize   ReadStream(byte []buffer, int nBuffSize)
    {
        if (s_readAr == null)
            s_readAr = new CSerialize(SerializeType.read, buffer, nBuffSize);
        else
            s_readAr.ResetStream(SerializeType.read, buffer, nBuffSize);
        return s_readAr;
    }
    static CSerialize s_writeAr;
    public static CSerialize WriteStream()
    {
        if (s_writeAr == null)
            s_writeAr = new CSerialize(SerializeType.write);
        else
            s_writeAr.ResetStream(SerializeType.write);
        return s_writeAr;
    }

    ~CSerialize()
    {
        if (m_arType != SerializeType.read && !string.IsNullOrEmpty(m_szFileName))
        {
            Close();
        }
    }

    public void SetVersion(int nVersion)
    {
        m_nVersion = nVersion;
    }
    public int GetVersion()
    {
        return m_nVersion;
    }

    public void Close()
    {
        Flush();
        m_bCreate = false;
        m_arType = SerializeType.read;
        m_szFileName = string.Empty;
    }
    
    public void Flush()
    {
        if (string.IsNullOrEmpty(m_szFileName))
            return;

        // 写入文件
        if (m_arType != SerializeType.read)
        {
            try
            {
                FileStream pFile = null;
                if (!m_bCreate && m_arType == SerializeType.write)
                {
                    m_bCreate = true;
                    if (File.Exists(m_szFileName))
                        File.Delete(m_szFileName);
                    pFile = File.Open(m_szFileName, FileMode.CreateNew, FileAccess.Write);
                }
                else
                {
                    if (GetBufferSize() == 0)
                        return;
                    pFile = File.Open(m_szFileName, FileMode.Append, FileAccess.Write);
                    if (pFile != null)
                    {
                        pFile.Seek(0, SeekOrigin.End);
                    }
                }

                if (pFile != null)
                {
                    if (GetBufferSize() > 0)
                        pFile.Write(GetBuffer(), 0, GetBufferSize());
                    pFile.Flush();
                    pFile.Close();
                    m_arFile.Flush();
                }
            }
            catch (Exception e)
            {
            }
        }
    }

    public byte[] GetBuffer()
    {
        return m_arFile.GetBuffer();
    }
    public int GetBufferSize()
    {
        return m_arFile.GetBufferSize();
    }
    public bool IsLoading()
    {
        return m_arType == SerializeType.read;
    }

    public void Read(ref int nValue)
    {
        m_arFile.ReadInt(ref nValue);
    }
	public void Read(ref uint nValue)
    {
        m_arFile.ReadInt(ref nValue);
    }
    public void Read(ref long nValue)
    {
        m_arFile.ReadInt(ref nValue);
    }
    public void Read(ref ulong nValue)
    {
        m_arFile.ReadInt(ref nValue);
    }
    public void Read(ref byte yValue)
    {
        m_arFile.ReadInt(ref yValue);
    }
    public void Read(ref short wValue)
    {
        m_arFile.ReadInt(ref wValue);
    }
    public void Read(ref ushort wValue)
    {
        m_arFile.ReadInt(ref wValue);
    }
    public void Read(ref bool bValue)
    {
        m_arFile.ReadInt(ref bValue);
    }
    public void Read(ref float fValue)
    {
        m_arFile.ReadInt(ref fValue);
    }
    public void Read(ref double fValue)
    {
        m_arFile.ReadInt(ref fValue);
    }
    public void Read(ref string str)
    {
        m_arFile.ReadString(ref str);
    }
    public void ReadStringUTF32(ref string str, int nLen)
    {
        m_arFile.ReadStringUTF32(ref str, nLen);
    }
    public void ReadStringUTF8(ref string str, int nLen)
    {
        m_arFile.ReadStringUTF8(ref str, nLen);
    }
    public void Read(ref byte[] vArray)
    {
        m_arFile.ReadIntArray(ref vArray);
    }
    public void Read(ref int[] vArray)
    {
        m_arFile.ReadIntArray(ref vArray);
    }
    public void Read(ref uint[] vArray)
    {
        m_arFile.ReadIntArray(ref vArray);
    }
    public void Read(ref float[] vArray)
    {
        m_arFile.ReadIntArray(ref vArray);
    }
    public void Read(ref string[] vArray)
    {
        m_arFile.ReadIntArray(ref vArray);
    }
    public void Read(ref Vector3[] vArray)
    {
        m_arFile.ReadIntArray(ref vArray);
    }
    public void Read(ref Color32[] vArray)
    {
        m_arFile.ReadIntArray(ref vArray);
    }
    public void ReadArray(ref byte []vArray, int nLen)
    {
        m_arFile.ReadArray(ref vArray, nLen);
    }
    public void Read(ref Vector3 tValue)
    {
        if (tValue == null)
            tValue = Vector3.zero;
        m_arFile.ReadInt(ref tValue.x);
        m_arFile.ReadInt(ref tValue.y);
        m_arFile.ReadInt(ref tValue.z);
    }
    public void Read(ref Quaternion tValue)
    {
        if (tValue == null)
            tValue = new Quaternion();
        m_arFile.ReadInt(ref tValue.x);
        m_arFile.ReadInt(ref tValue.y);
        m_arFile.ReadInt(ref tValue.z);
        m_arFile.ReadInt(ref tValue.w);
    }
    public void Read(ref Rect tValue)
    {
        if (tValue == null)
            tValue = new Rect();
        float xMin = 0.0f, yMin = 0.0f, xMax = 0.0f, yMax = 0.0f;
        m_arFile.ReadInt(ref xMin);
        m_arFile.ReadInt(ref yMin);
        m_arFile.ReadInt(ref xMax);
        m_arFile.ReadInt(ref yMax);
        tValue.xMin = xMin;
        tValue.yMin = yMin;
        tValue.xMax = xMax;
        tValue.yMax = yMax;
    }
    public void Read(ref Bounds tValue)
    {
        if (tValue == null)
            tValue = new Bounds();
        Vector3 vMin = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 vMax = new Vector3(0.0f, 0.0f, 0.0f);
        Read(ref vMin);
        Read(ref vMax);
        tValue.SetMinMax(vMin, vMax);
    }
    public  void Read(ref Color32 tValue)
    {
        m_arFile.ReadInt(ref tValue);
    }

    public void Write(int nValue)
    {
        m_arFile.WriteInt(nValue);
    }
	public void Write(uint nValue)
    {
        m_arFile.WriteInt(nValue);
    }
    public void Write(long nValue)
    {
        m_arFile.WriteInt(nValue);
    }
    public void Write(ulong nValue)
    {
        m_arFile.WriteInt(nValue);
    }
    public void Write(byte yValue)
    {
        m_arFile.WriteInt(yValue);
    }
    public void Write(short wValue)
    {
        m_arFile.WriteInt(wValue);
    }
    public void Write(ushort wValue)
    {
        m_arFile.WriteInt(wValue);
    }
    public void Write(bool bValue)
    {
        m_arFile.WriteInt(bValue);
    }
    public void Write(float fValue)
    {
        m_arFile.WriteInt(fValue);
    }
    public void Write(double fValue)
    {
        m_arFile.WriteInt(fValue);
    }
    public void Write(string str)
    {
        m_arFile.WriteString(str);
    }
    public void PushTextString(string str)
    {
        m_arFile.PushTextString(str);
    }
    public void PushTextString(byte[] str)
    {
        m_arFile.PushTextString(str);
    }
    public void WriteStringUTF32(string str, int nSize)
    {
        m_arFile.WriteStringUTF32(str, nSize);
    }
    public void WriteStringUTF8(string str, int nSize)
    {
        m_arFile.WriteStringUTF8(str, nSize);
    }
    public void Write(byte[] vArray)
    {
        m_arFile.WriteIntArray(vArray);
    }
    public void Write(int[] vArray)
    {
        m_arFile.WriteIntArray(vArray);
    }
    public void Write(uint[] vArray)
    {
        m_arFile.WriteIntArray(vArray);
    }
    public void Write(float[] vArray)
    {
        m_arFile.WriteIntArray(vArray);
    }
    public void Write(string[] vArray)
    {
        m_arFile.WriteIntArray(vArray);
    }
    public void Write(Vector3[] vArray)
    {
        m_arFile.WriteIntArray(vArray);
    }
    public void Write(Color32 []vArray)
    {
        m_arFile.WriteIntArray(vArray);
    }
    public void WriteArray(byte []vArray, int nLen)
    {
        m_arFile.WriteArray(vArray, nLen);
    }
    public void Write(Vector3 v)
    {
        m_arFile.WriteInt(v.x);
        m_arFile.WriteInt(v.y);
        m_arFile.WriteInt(v.z);
    }
    public void Write(Quaternion v)
    {
        m_arFile.WriteInt(v.x);
        m_arFile.WriteInt(v.y);
        m_arFile.WriteInt(v.z);
        m_arFile.WriteInt(v.w);
    }
    public void Write(Rect tValue)
    {
        m_arFile.WriteInt(tValue.xMin);
        m_arFile.WriteInt(tValue.yMin);
        m_arFile.WriteInt(tValue.xMax);
        m_arFile.WriteInt(tValue.yMax);
    }
    public void Write(Bounds v)
    {
        Write(v.min);
        Write(v.max);
    }
    public void Write(Color32 v)
    {
        m_arFile.WriteInt(v);
    }

    //-----------------------------------------------
    public byte ReadByte(byte yDef = 0)
    {
        m_arFile.ReadInt(ref yDef);
        return yDef;
    }
    public bool ReadBool(bool bDef = false)
    {
        m_arFile.ReadInt(ref bDef);
        return bDef;
    }
    public short ReadInt16(short nDef = 0)
    {
        m_arFile.ReadInt(ref nDef);
        return nDef;
    }
    public int ReadInt32(int nDef = 0)
    {
        m_arFile.ReadInt(ref nDef);
        return nDef;
    }
    public float ReadFloat(float fDef = 0.0f)
    {
        m_arFile.ReadInt(ref fDef);
        return fDef;
    }
    public string ReadString(string szDef = "")
    {
        m_arFile.ReadString(ref szDef);
        return szDef;
    }
    //-----------------------------------------------
    public void ReadWriteValue(ref bool tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref byte tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref short tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref ushort tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref int tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
	public void ReadWriteValue(ref uint tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref long tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref ulong tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref float tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref double tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref string tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref byte[] tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref int[] tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref uint[] tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref float[] tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref string[] tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref Vector3[] tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref Color32[] tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref Vector3 tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref Quaternion tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }    
    public void ReadWriteValue(ref Rect tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref Bounds tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteValue(ref Color32 tValue)
    {
        if (IsLoading())
            Read(ref tValue);
        else
            Write(tValue);
    }
    public void ReadWriteStringUTF32(ref string tValue, int nSize)
    {
        if (IsLoading())
            ReadStringUTF32(ref tValue, nSize);
        else
            WriteStringUTF32(tValue, nSize);
    }
    public void ReadWriteStringUTF8(ref string tValue, int nSize)
    {
        if (IsLoading())
            ReadStringUTF8(ref tValue, nSize);
        else
            WriteStringUTF8(tValue, nSize);
    }
    public void ReadWriteArray(ref byte []tValue, int nSize)
    {
        if (IsLoading())
            ReadArray(ref tValue, nSize);
        else
            WriteArray(tValue, nSize);
    }
    public delegate void SerializeValue<_Ty>(CSerialize ar, ref _Ty tValue);
    public void ReadWriteValue<_Ty>(ref _Ty tValue, SerializeValue<_Ty> func) where _Ty:new ()
    {
        if (tValue == null)
            tValue = new _Ty();
        func(this, ref tValue);
    }

    public void ReadWriteValue<_Ty>(ref _Ty tValue) where _Ty : ISerializable,new()
    {
        if (tValue == null)
            tValue = new _Ty();
        tValue.Serialize(this);
    }

    public void AutoNewValue<_Ty>(ref _Ty tValue) where _Ty : new()
    {
        if (tValue == null)
            tValue = new _Ty();
    }

    public void SerializeArray<_Ty>(ref _Ty[] aValue)
    {
        int nLen = 0;
        if (IsLoading())
        {
            Read(ref nLen);
            if (nLen < 0 || nLen > 1024 * 1024)
                nLen = 0;
            if (nLen <= 0)
                aValue = new _Ty[0];
            else
            {
                aValue = new _Ty[nLen];
            }
        }
        else
        {
            nLen = aValue != null ? aValue.Length : 0;
            Write(nLen);
        }
    }

    public void SerializeBaseArray(ref List<string> aValue)
    {
        int nLen = 0;
        if (IsLoading())
        {
            Read(ref nLen);
            if (nLen < 0 || nLen > 1024 * 1024)
                nLen = 0;
            if (aValue == null)
            {
                aValue = new List<string>();
            }
            aValue.Clear();
            for (int i = 0; i < nLen; ++i)
            {
                string tmpStr = string.Empty;
                this.Read(ref tmpStr);
                aValue.Add(tmpStr);
            }
        }
        else
        {
            nLen = aValue != null ? aValue.Count : 0;
            Write(nLen);
            for (int i=0;i<nLen;++i)
            {
                this.Write(aValue[i]);
            }
        }
    }

    public void SerializeBaseArray(ref List<int> aValue)
    {
        int nLen = 0;
        if (IsLoading())
        {
            Read(ref nLen);
            if (nLen < 0 || nLen > 1024 * 1024)
                nLen = 0;
            if (aValue == null)
            {
                aValue = new List<int>();
            }
            aValue.Clear();
            for (int i = 0; i < nLen; ++i)
            {
                int tmp = 0;
                this.Read(ref tmp);
                aValue.Add(tmp);
            }
        }
        else
        {
            nLen = aValue != null ? aValue.Count : 0;
            Write(nLen);
            for (int i=0;i<nLen;++i) {
                this.Write(aValue[i]);
            }
        }
    }



    public delegate void SerializeArrayNode<_Ty>(CSerialize ar, ref _Ty value);
    public void SerializeArray<_Ty>(ref _Ty[] aValue, SerializeArrayNode<_Ty> serializeFunc)
    {
        int nLen = 0;
        if (IsLoading())
        {
            Read(ref nLen);
            if (nLen < 0 || nLen > 1024 * 1024)
                nLen = 0;
            if (nLen <= 0)
                aValue = new _Ty[0];
            else
            {
                aValue = new _Ty[nLen];
            }
        }
        else
        {
            nLen = aValue != null ? aValue.Length : 0;
            Write(nLen);
        }
        for (int i = 0; i < nLen; ++i)
        {
            serializeFunc(this, ref aValue[i]);
        }
    }

    // 功能：读取一个对象数组
    public void SerializeObjectArray<_Ty>(ref _Ty[] aValue) where _Ty : ISerializable, new()
    {
        int nLen = 0;
        if (IsLoading())
        {
            Read(ref nLen);
            if (nLen < 0 || nLen > 1024 * 1024)
                nLen = 0;
            if (nLen > 0)
                aValue = new _Ty[nLen];
            else
                aValue = null;
            for (int i = 0; i < nLen; ++i)
            {
                aValue[i] = new _Ty();
                aValue[i].Serialize(this);
            }
        }
        else
        {
            nLen = aValue != null ? aValue.Length : 0;
            Write(nLen);
            for (int i = 0; i < nLen; ++i)
            {
                _Ty value = aValue[i];
                if (value == null)
                    value = new _Ty();
                value.Serialize(this);
            }
        }
    }

    public void SerializeArray<_Ty>(ref List<_Ty> aValue, SerializeArrayNode<_Ty> serializeFunc) where _Ty : new()
    {
        if (aValue == null)
            aValue = new List<_Ty>();

        int nLen = 0;
        if (IsLoading())
        {
            Read(ref nLen);
            if (nLen < 0 || nLen > 1024 * 1024)
                nLen = 0;
            aValue.Clear();
            for (int i = 0; i < nLen; ++i)
            {
                _Ty value = new _Ty();
                serializeFunc(this, ref value);
                if (value != null)
                {
                    aValue.Add(value);
                }
            }
        }
        else
        {
            nLen = aValue != null ? aValue.Count : 0;
            Write(nLen);
            for (int i = 0; i < nLen; ++i)
            {
                _Ty value = aValue[i];
                serializeFunc(this, ref value);
            }
        }
    }

    public void SerializeStructArray<_Ty>(ref List<_Ty> aValue) where _Ty : ISerializable, new()
    {
        if (aValue == null)
            aValue = new List<_Ty>();
        int nLen = 0;
        if (IsLoading())
        {
            Read(ref nLen);
            if (nLen < 0 || nLen > 1024 * 1024)
                nLen = 0;
            aValue.Clear();
            for (int i = 0; i < nLen; ++i)
            {
                _Ty value = new _Ty();
                value.Serialize(this);
                aValue.Add(value);
            }
        }
        else
        {
            nLen = aValue != null ? aValue.Count : 0;
            Write(nLen);
            for (int i = 0; i < nLen; ++i)
            {
                _Ty value = aValue[i];
                value.Serialize(this);
            }
        }
    }
    
    public delegate void SerializeIterator<_TyKey, _TyValue>(CSerialize ar, ref _TyKey key, ref _TyValue value);
    public void SerializeDictionary<_TyKey, _TyValue>(ref Dictionary<_TyKey, _TyValue> aValue, SerializeIterator<_TyKey, _TyValue> serializeFunc)
    {
        if (aValue == null)
            aValue = new Dictionary<_TyKey, _TyValue>();

        int nLen = 0;
        if (IsLoading())
        {
            Read(ref nLen);
            if (nLen < 0 || nLen > 1024 * 1024)
                nLen = 0;
            if (aValue == null)
                aValue = new Dictionary<_TyKey, _TyValue>();
            aValue.Clear();
            for (int i = 0; i < nLen; ++i)
            {
                _TyKey key = default(_TyKey);
                _TyValue value = default(_TyValue);
                serializeFunc(this, ref key, ref value);
                if( key != null && value != null )
                    aValue[key] = value;
            }
        }
        else
        {
            nLen = aValue != null ? aValue.Count : 0;
            Write(nLen);
            if (nLen > 0)
            {
                Dictionary<_TyKey, _TyValue>.Enumerator it = aValue.GetEnumerator();
                while (it.MoveNext())
                {
                    _TyKey key = it.Current.Key;
                    _TyValue value = it.Current.Value;
                    serializeFunc(this, ref key, ref value);
                }
            }
        }
    }
    //-----------------------------------------------
    //public static CSerialize operator >>(CSerialize ar, int nValue)
    //{
    //    return ar;
    //}
};

// 测试发现，如果参数是传数组类，只要不修改数组长度，不对数组重新赋值，修改期中的变量，外部是可以得到变化的
// 换言之，数组如果只修改成员的话，是可以不用传引用的