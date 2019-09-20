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

public class myAdsAdapter extends ArrayAdapter<String> {

    private ArrayList<String> types;
    private ArrayList<String> discs;

    private ArrayList<String> stateTypes;
    private Activity context;

    Bitmap bitmap;

    ArrayList<Bitmap> bitmaps;


    public myAdsAdapter(Activity context, ArrayList<String> types, ArrayList<String>  discs, ArrayList<Bitmap>  bitmaps,ArrayList<String> stateTypes) {
        super(context, R.layout.activity_my_ads,types);
        this.context=context;
        this.types =types;
        this.discs =discs;
        this.bitmaps = bitmaps;
        this.stateTypes = stateTypes;
    }
    @Override
    public View getView(int position, View convertView, ViewGroup parent){
        View r=convertView;
        myAdsAdapter.ViewHoldeAccaunt viewHolder=null;
        if(r==null){
            LayoutInflater layoutInflater=context.getLayoutInflater();
            r=layoutInflater.inflate(R.layout.my_realtys_layout,null,true);
            viewHolder=new myAdsAdapter.ViewHoldeAccaunt(r);
            r.setTag(viewHolder);
        }
        else {
            viewHolder=(myAdsAdapter.ViewHoldeAccaunt)r.getTag();

        }

        viewHolder.tvw1.setText(types.get(position));
        viewHolder.tvw2.setText(discs.get(position));
        viewHolder.im.setImageBitmap(bitmaps.get(position));



        return r;
    }

    @Override
    public int getViewTypeCount() {
        // menu type count
        return 3;
    }

    @Override
    public int getItemViewType(int position) {

        return Integer.parseInt(stateTypes.get(position))-1;
    }
    class ViewHoldeAccaunt{

        TextView tvw1;
        TextView tvw2;
        ImageView im;



        ViewHoldeAccaunt(View v){
            tvw1=(TextView)v.findViewById(R.id.typeDial);
            tvw2=(TextView)v.findViewById(R.id.discription);
            im=(ImageView)v.findViewById(R.id.photo);


        }
    }
}
