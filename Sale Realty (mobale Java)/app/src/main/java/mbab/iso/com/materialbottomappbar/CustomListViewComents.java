package mbab.iso.com.materialbottomappbar;

import android.app.Activity;
import android.graphics.Bitmap;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;

public class CustomListViewComents extends ArrayAdapter<String> {
    private ArrayList<String> logins;
    private ArrayList<String> coments;
    private Activity context;

    public CustomListViewComents(Activity context, ArrayList<String> logins, ArrayList<String> coments) {
        super(context, R.layout.coment_layout,logins);
        this.context=context;
        this.logins=logins;
        this.coments=coments;

    }


    @Override
    public View getView(int position,  View convertView,  ViewGroup parent){
        View r=convertView;
        CustomListViewComents.ViewHolderComents viewHolder=null;
        if(r==null){
            LayoutInflater layoutInflater=context.getLayoutInflater();
            r=layoutInflater.inflate(R.layout.coment_layout,null,true);
            viewHolder=new CustomListViewComents.ViewHolderComents(r);
            r.setTag(viewHolder);
        }
        else {
            viewHolder=(CustomListViewComents.ViewHolderComents)r.getTag();

        }

        viewHolder.tvw1.setText(logins.get(position));
        viewHolder.tvw2.setText(coments.get(position));


        return r;
    }

    class ViewHolderComents{

        TextView tvw1;
        TextView tvw2;

        ViewHolderComents(View v){
            tvw1=(TextView)v.findViewById(R.id.login);
            tvw2=(TextView)v.findViewById(R.id.coment);
        }
    }
}
