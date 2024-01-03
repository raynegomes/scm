namespace AppManager.Infra.Data.Mapping;
public sealed class TableNames
{
	public string Value { get; private set; }
	public override string ToString() => Value;

	private TableNames(string value) 
	{ 
		Value = value; 
	}

	#region Tables Name

	public static TableNames UserTable { get { return new TableNames("users"); } }

	#endregion

}
