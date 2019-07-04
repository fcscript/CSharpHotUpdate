
int  Power(int x, int n)
{
    int nTotal = 1;
    for(int i = 0; i<n; ++i)
    {
        nTotal *= x;
    }
    return nTotal;
}

export void  main()
{
    print("hello world.");

    int  i = 100;
    i += 100;
    print("i = {0}", i);
    i += 1 << 12;
    print("i = {0}", i);
    print("i = {0}", i + 1);
    
    print("Power(2, 5)={0}", Power(2,5));
}
