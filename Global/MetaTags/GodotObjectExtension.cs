public static class GodotObjectExtension
{
	public const string META_TAG_KEY = "GodotObjectExtensionMetaTagToolGame";
	public static void SetTag(this GodotObject obj, ETag tag, bool enabled)
	{
		obj.SetMeta(META_TAG_KEY+tag.ToString(), enabled);
	}

	public static bool HasTag(this GodotObject obj, ETag tag, bool enabled)
	{
		return (bool)obj.GetMeta(META_TAG_KEY+tag.ToString(), false);
	}	
}
