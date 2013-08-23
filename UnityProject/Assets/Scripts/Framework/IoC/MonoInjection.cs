using UnityEngine;

public static class MonoInjection
{
	public static void Inject(this MonoBehaviour script)
	{
		script.SendMessageUpwards(UnityContext.ADDED, script, SendMessageOptions.RequireReceiver);
	}
}