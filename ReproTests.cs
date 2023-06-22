namespace mstest_readonly_struct_repro;

[TestClass]
public class ReproTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestDataDateOnly), DynamicDataSourceType.Method)]
    public void TestDateOnly(DateOnly today, DateOnly inForceFrom)
    {
        Assert.AreNotEqual(new DateOnly(), today);
        Assert.AreEqual(new DateOnly(2016, 4, 25), today);
    }

    public static IEnumerable<object[]> TestDataDateOnly()
    {
        yield return new object[] { new DateOnly(2016, 4, 25), new DateOnly(2016, 4, 26) };
    }

    [DataTestMethod]
    [DynamicData(nameof(TestDataCustomRecordStruct), DynamicDataSourceType.Method)]
    public void TestCustomRecordStruct(MyStructField today)
    {
        Assert.AreNotEqual(new MyStructField(), today);
        Assert.AreEqual(new MyStructField(2016), today);
    }

    public static IEnumerable<object[]> TestDataCustomRecordStruct()
    {
        yield return new object[] { new MyStructField(2016) };
    }

    [DataTestMethod]
    [DynamicData(nameof(TestDataCustomRecordStructProp), DynamicDataSourceType.Method)]
    public void TestCustomRecordStructProp(MyStructProp today)
    {
        Assert.AreNotEqual(new MyStructInitProp(), today);
        Assert.AreEqual(new MyStructProp(2016), today);
    }

    public static IEnumerable<object[]> TestDataCustomRecordStructProp()
    {
        yield return new object[] { new MyStructProp(2016) };
    }

    // Works
    [DataTestMethod]
    [DynamicData(nameof(TestDataCustomRecordStructInitProp), DynamicDataSourceType.Method)]
    public void TestCustomRecordStructInitProp(MyStructInitProp today)
    {
        Assert.AreNotEqual(new MyStructInitProp(), today);
        Assert.AreEqual(new MyStructInitProp(2016), today);
    }

    public static IEnumerable<object[]> TestDataCustomRecordStructInitProp()
    {
        yield return new object[] { new MyStructInitProp(2016) };
    }
}

public readonly struct MyStructField
{
    private readonly int _i;

    public MyStructField(int i)
    {
        _i = i;
    }

    public override string ToString()
    {
        return $"_i: {_i}";
    }
}

public readonly struct MyStructProp
{
    public int I { get; }

    public MyStructProp(int i)
    {
        I = i;
    }

    public override string ToString()
    {
        return $"_i: {I}";
    }
}

// or just public readonly record struct MyStructInitProp(int I)
public readonly struct MyStructInitProp
{
    public int I { get; init; }

    public MyStructInitProp(int i)
    {
        I = i;
    }

    public override string ToString()
    {
        return $"_i: {I}";
    }
}