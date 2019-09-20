package mbab.iso.com.materialbottomappbar;

import android.app.Service;
import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;

public class ConectionDetector {

    Context context;

    public ConectionDetector(Context context)
    {
        this.context = context;
    }

    public boolean isConected()
    {
        ConnectivityManager connectivityManager = (ConnectivityManager)
                context.getSystemService(Service.CONNECTIVITY_SERVICE);
        if(connectivityManager==null)
            return false;
        NetworkInfo info = connectivityManager.getActiveNetworkInfo();
        if(info==null)
            return false;
        return info.getState()==NetworkInfo.State.CONNECTED;
    }
}
