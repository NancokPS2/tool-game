public static class CollectionExtension
{
	public static string ToStringList<T>(this ICollection<T> collection)
	{
		string output = "";
		foreach (var item in collection)
		{
			output += item?.ToString() ?? throw new Exception();
		}
		return output;
	}
}
