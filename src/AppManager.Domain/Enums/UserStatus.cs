using System.ComponentModel;

namespace AppManager.Domain.Enums;

public enum UserStatus
{
	[Description("Inactive")]
	Inactive,
	[Description("Active")]
	Active,
	[Description("Suspended")]
	Suspended
}
