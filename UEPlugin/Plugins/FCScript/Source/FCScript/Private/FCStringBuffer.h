#pragma once
#include <string>

template <int BufferSize>
class FCStringBuffer
{
    char *mBuffer;
    int   mSize;
    int   mMaxSize;
    char  TempBuffer[BufferSize];
public:
    FCStringBuffer():mBuffer(nullptr), mSize(0), mMaxSize(0)
    {
        mBuffer = TempBuffer;
        mMaxSize = BufferSize;
        TempBuffer[0] = 0;
    }
    ~FCStringBuffer()
    {
        if(mBuffer != TempBuffer)
        {
            delete []mBuffer;
        }
    }
    FCStringBuffer(const FCStringBuffer &Other):mBuffer(nullptr), mSize(0), mMaxSize(0)
    {
        mBuffer = TempBuffer;
        mMaxSize = BufferSize;
        TempBuffer[0] = 0;
        Copy(Other);
    }
    FCStringBuffer &operator = (const FCStringBuffer &Other)
    {
        Copy(Other);
        return *this;
    }
    void  Copy(const FCStringBuffer &Other)
    {
        if(this != &Other)
        {
            Reserve(Other.mSize + 1);
            mSize = Other.mSize;
            memcpy(mBuffer, Other.mBuffer, Other.mSize);
            mBuffer[mSize] = 0;
        }
    }
public:
    operator const char *()
    {
        return mBuffer;
    }
    const char *GetString() const
    {
        return mBuffer;
    }
    int   Size()
    {
        return mSize;
    }
    void  Empty()
    {
        mSize = 0;
        mBuffer[0] = 0;
    }
    void  Reserve(int nNewSize)
    {
        if(nNewSize < mMaxSize)
        {
            return ;        
        }
        char  *NewBuffer = new char[nNewSize+1];
        memcpy(NewBuffer, mBuffer, mSize);
        if(mBuffer != TempBuffer)
        {
            delete[]mBuffer;
        }
        mBuffer = NewBuffer;
        mMaxSize = nNewSize;
        mBuffer[mSize] = 0;
    }
    void  PushChar(char ch)
    {
        if(mSize >= mMaxSize)
        {
            Reserve(mMaxSize * 2);
        }
        mBuffer[mSize++] = ch;
        mBuffer[mSize] = 0;
    }
    void  PushStr(const char *InStr, int InLen)
    {
        if(InStr && InLen > 0)
        {
            Reserve(mSize + InLen + 1);
            memcpy(mBuffer + mSize, InStr, InLen);
            mSize += InLen;
            mBuffer[mSize] = 0;
        }
    }
    void PushInt64(int64 Value)
    {
        if(Value < 0)
        {
            PushChar('-');
            Value = -Value;
        }
        char   Temp[64];
        int Count = 0;
        do
        {
            Temp[Count++] = (Value % 10) + '0';
            Value /= 10;
        }while(Value > 0);
        
        Reserve(mSize + Count + 1);
        for(int i = Count - 1; i>= 0; --i)
        {
            mBuffer[mSize++] = Temp[i];
        }
        mBuffer[mSize] = 0;
    }
    const char* ReadValue(const char *InStr, char chEnd)
    {        
        Empty();
        if(!InStr)
            return InStr;
        for(; *InStr; ++InStr)
        {
            if(*InStr == chEnd)
            {
                ++InStr;
                break;
            }
            PushChar(*InStr);
        }
        return InStr;
    }
    float ReadFloat(const char *&InStr, char chEnd = ',')
    {
        InStr = ReadValue(InStr, chEnd);
        return atof(mBuffer);
    }
    FCStringBuffer &operator << (const char *InStr)
    {
        if(!InStr)
        {
            return *this;
        }
        int Len = strlen(InStr);
        Reserve(mSize + Len + 1);
        for(; *InStr; ++InStr)
        {
            mBuffer[mSize++] = *InStr;
        }
        mBuffer[mSize] = 0;
        return *this;
    }
    FCStringBuffer& operator << (int64 Value)
    {
        PushInt64(Value);
        return *this;
    }
};

typedef FCStringBuffer<128>  FCStringBuffer128;