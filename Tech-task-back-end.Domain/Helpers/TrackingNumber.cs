namespace Tech_task_back_end.Domain.Helpers;

public static class TrackingNumber
{
    /// <summary>
    /// Computes a tracking number from a package's ID. <br/>
    /// The ID of the package is mildly obfuscated by combining it with a pseudo-random value and hashing
    /// the combination with CRC-64.
    /// </summary>
    /// <param name="packageId">ID of the package to create a tracking ID for</param>
    /// <returns>The tracking number, represented by a semi-unique 17 character string</returns>
    public static string Create(int packageId)
    {
        byte[] idBytes = BitConverter.GetBytes(packageId);

        byte[] tickBytes = BitConverter.GetBytes(DateTime.Now.Microsecond);

        byte[] joined = idBytes.Concat(tickBytes).ToArray();

        byte[] hash = System.IO.Hashing.Crc64.Hash(joined);

        string trackingNum = Convert.ToHexString(hash).Insert(8, "-");

        return trackingNum;
    }
}