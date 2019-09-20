package mbab.iso.com.materialbottomappbar;

import android.app.Activity;
import android.graphics.Bitmap;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

import java.util.ArrayList;

public class accauntListViewAdapter extends ArrayAdapter<String> {

    private String[] profilename;
    private String[] count;

    private Activity context;
    Bitmap bitmap;
    Bitmap[] bitmaps;
    int[] imgs;

    public accauntListViewAdapter(Activity context, String[] profilename,String[] count, int[]imgs) {
        super(context, R.layout.layout,profilename);
        this.context=context;
        this.profilename=profilename;
        this.count=count;
        this.imgs = imgs;
    }
    @Override
    public View getView(int position, View convertView, ViewGroup parent){
        View r=convertView;
        accauntListViewAdapter.ViewHoldeAccaunt viewHolder=null;
        if(r==null){
            LayoutInflater layoutInflater=context.getLayoutInflater();
            r=layoutInflater.inflate(R.layout.acccaun_list_view_adapter,null,true);
            viewHolder=new accauntListViewAdapter.ViewHoldeAccaunt(r);
            r.setTag(viewHolder);
        }
        else {
            viewHolder=(accauntListViewAdapter.ViewHoldeAccaunt)r.getTag();

        }

        viewHolder.tvw1.setText(profilename[position]);
        viewHolder.tvw2.setText(count[position]);
        viewHolder.lv.setImageResource(imgs[position]);

        if(position>=3)
            viewHolder.lin.setVisibility(View.VISIBLE);

        else
            {
                viewHolder.tvw3.setText(count[position]);
                viewHolder.tvw3.setVisibility(View.VISIBLE);
            }

        if(position==0)
            viewHolder.lin.setVisibility(View.GONE);


        return r;
    }

    class ViewHoldeAccaunt{

        TextView tvw1;
        TextView tvw2;
        TextView tvw3;
        LinearLayout lin;
        ImageView lv;


        ViewHoldeAccaunt(View v){
            tvw1=(TextView)v.findViewById(R.id.accauntButtonName);
            tvw2=(TextView)v.findViewById(R.id.countView);
            tvw3=(TextView)v.findViewById(R.id.accauntText);
            lin=(LinearLayout)v.findViewById(R.id.countLayout);
            lv=(ImageView)v.findViewById(R.id.icon);

        }
    }
}
