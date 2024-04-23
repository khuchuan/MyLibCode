using IdGen;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace LibUtil;


public static class SnowFlakIdGenerator
{
    private static readonly IdGenerator _generator;
    private static byte bit_timestamp = 43; //2^43
    private static byte bit_generalid = 8; //2^8
    private static byte bit_sequence = 12; //2^12
    private static DateTime epochTime = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Local);
    static SnowFlakIdGenerator()
    {
        // Read `GENERATOR_ID` from .env file in service root folder or system environment variables
        //DotNetEnv.Env.GetInt("GENERATOR_ID", 0);

        //var generatorId = Convert.ToByte(ConfigurationHelper.config["Appsettings:PartitionID"]);
        var generatorId = 2;

        // Let's say we take jan 17st 2022 as our epoch
        var epoch = epochTime;

        // Create an ID with 45 bits for timestamp, 2 for generator-id
        // and 16 for sequence
        var structure = new IdStructure(bit_timestamp, bit_generalid, bit_sequence);

        // Prepare options
        var options = new IdGeneratorOptions(structure, new DefaultTimeSource(epoch));

        // Create an IdGenerator with it's generator-id set to 0, our custom epoch
        // and id-structure
        _generator = new IdGenerator(generatorId, options);
    }

    public static long NewId()
    {
        return _generator.CreateId();
    }

    public static object DecodeFromId(long id)
    {
        int SHIFT_TIME = bit_generalid + bit_sequence;
        int SHIFT_GENERATOR = bit_sequence;
        long MASK_TIME = GetMaskBits(bit_timestamp);
        long MASK_GENERATOR = GetMaskBits(bit_generalid);
        long MASK_SEQUENCE = GetMaskBits(bit_sequence);
        var time = new DefaultTimeSource(epochTime);
        var seq_id = (int)(id & MASK_SEQUENCE);
        var gen_id = (int)((id >> SHIFT_GENERATOR) & MASK_GENERATOR);
        var timeEpoch = time.Epoch.Add(TimeSpan.FromTicks(((id >> SHIFT_TIME) & MASK_TIME) * time.TickDuration.Ticks));
        return new { seq_id, gen_id, timeEpoch };
    }
    public static int GetPartitionNode(long id)
    {
        dynamic data = DecodeFromId(id);
        return ObjectHelper.GetPropValue(data, "gen_id") ?? 0;
    }
    private static long GetMaskBits(byte bits)
    {
        return (1L << (int)bits) - 1;
    }
}



public static class ConfigurationHelper
{
    public static IConfiguration config;
    public static void Initialize(IConfiguration Configuration)
    {
        config = Configuration;
    }
}

public static class ObjectHelper
{
    public static object GetPropValue(object src, string propName)
    {
        return src.GetType().GetProperty(propName).GetValue(src, null);
    }
    public static T CastTo<T>(this Object value, T targetType) => (T)value;
}