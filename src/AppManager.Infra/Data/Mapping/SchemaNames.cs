namespace AppManager.Infra.Data.Mapping;

public sealed class SchemaNames
{
	public string Value { get; private set; }

	public override string ToString() => Value;

	public SchemaNames(string value)
	{
		Value = value;
	}

	#region Schemas Names
	public static SchemaNames DefaultSchema { get { return new SchemaNames("dbo"); } }
	public static SchemaNames AccountSchema { get { return new SchemaNames("account"); } }

	#endregion
}
