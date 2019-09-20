package mbab.iso.com.materialbottomappbar;

import android.app.Activity;
import android.graphics.Bitmap;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import java.util.ArrayList;

public class listItemAdapter extends ArrayAdapter<String> {
    private ArrayList<String>  profilename;

    private Activity context;
    Bitmap bitmap;
    Bitmap[] bitmaps;

    public listItemAdapter(Activity context, ArrayList<String> profilename) {
        super(context, R.layout.layout,profilename);
        this.context=context;
        this.profilename=profilename;
    }
    @Override
    public View getView(int position,  View convertView,  ViewGroup parent){
        View r=convertView;
        listItemAdapter.ViewHolder viewHolder=null;
        if(r==null){
            LayoutInflater layoutInflater=context.getLayoutInflater();
            r=layoutInflater.inflate(R.layout.list_adapter,null,true);
            viewHolder=new listItemAdapter.ViewHolder(r);
            r.setTag(viewHolder);
        }
        else {
            viewHolder=(listItemAdapter.ViewHolder)r.getTag();

        }

        viewHolder.tvw1.setText(profilename.get(position));

        return r;
    }

    class ViewHolder{

        TextView tvw1;


        ViewHolder(View v){
            tvw1=(TextView)v.findViewById(R.id.listItem);

        }
    }

}
