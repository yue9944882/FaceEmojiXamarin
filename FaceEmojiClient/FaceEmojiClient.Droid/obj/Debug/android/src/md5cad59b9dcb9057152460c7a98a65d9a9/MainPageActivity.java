package md5cad59b9dcb9057152460c7a98a65d9a9;


public class MainPageActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("FaceEmojiClient.Droid.MainPageActivity, FaceEmojiClient.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainPageActivity.class, __md_methods);
	}


	public MainPageActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainPageActivity.class)
			mono.android.TypeManager.Activate ("FaceEmojiClient.Droid.MainPageActivity, FaceEmojiClient.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
