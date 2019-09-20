package mbab.iso.com.materialbottomappbar;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Typeface;
import android.os.AsyncTask;
import android.text.SpannableString;
import android.text.style.StyleSpan;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import java.io.InputStream;
import java.util.ArrayList;

public class CustomListView extends ArrayAdapter<String> {
    private ArrayList<String> profilename;
    private ArrayList<String> email;
    private ArrayList<String> imagepath;
    private Activity context;
    Bitmap bitmap;
    ArrayList<Bitmap> bitmaps;

    public CustomListView(Activity context, ArrayList<String> profilename, ArrayList<String> email, ArrayList<String> imagepath,ArrayList<Bitmap> bitmaps) {
        super(context, R.layout.layout,profilename);
        this.context=context;
        this.profilename=profilename;
        this.email=email;
        this.imagepath=imagepath;
        this.bitmaps = bitmaps;
    }


    @Override
    public View getView(int position,  View convertView,  ViewGroup parent){
        View r=convertView;
        ViewHolder viewHolder=null;
        if(r==null){
            LayoutInflater layoutInflater=context.getLayoutInflater();
            r=layoutInflater.inflate(R.layout.layout,null,true);
            viewHolder=new ViewHolder(r);
            r.setTag(viewHolder);
        }
        else {
            viewHolder=(ViewHolder)r.getTag();

        }


        SpannableString spanString = new SpannableString(profilename.get(position)+" p.");
        spanString.setSpan(new StyleSpan(Typeface.BOLD), 0, spanString.length(), 0);
        spanString.setSpan(new StyleSpan(Typeface.ITALIC), 0, spanString.length(), 0);
        viewHolder.tvw1.setText(spanString);

        viewHolder.tvw2.setText(email.get(position));
        viewHolder.ivw.setImageBitmap( bitmaps.get(position));
//        new GetImageFromURL(viewHolder.ivw ).execute(imagepath[position]);

        return r;
    }

    class ViewHolder{

        TextView tvw1;
        TextView tvw2;
        ImageView ivw;

        ViewHolder(View v){
            tvw1=(TextView)v.findViewById(R.id.tvprofilename);
            tvw2=(TextView)v.findViewById(R.id.tvemail);
            ivw=(ImageView)v.findViewById(R.id.imageView);
        }
    }


}



