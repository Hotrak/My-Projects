package mbab.iso.com.materialbottomappbar;

import android.content.Context;
import android.graphics.Bitmap;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.viewpager.widget.PagerAdapter;

import java.util.ArrayList;
import java.util.List;

//import android.annotation.NonNull;
//import androidx.viewpager.widget.PagerAdapter;

public class SliderAdapter extends PagerAdapter {

    private Context context;
    private LayoutInflater layoutInflater;
    private ArrayList<Bitmap> bitmaps;

    public SliderAdapter(Context context, ArrayList<Bitmap> bitmaps)
    {
        this.context = context;
        this.bitmaps = bitmaps;
    }

//    public int[] slide_images =
//    {
//        R.drawable.ic_hor, R.drawable.id_pop, R.drawable.id_rop
//    };

    private String[] Text =
    {
            "111111", "22222", "333333"
    };

    @Override
    public int getCount() {
        return bitmaps.size();
    }

    @Override
    public boolean isViewFromObject(@NonNull View view, @NonNull Object object) {

        return view == (RelativeLayout)object;
    }
    @NonNull
    @Override
    public Object instantiateItem(@NonNull ViewGroup container, int position)
    {
        layoutInflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        View view =layoutInflater.inflate(R.layout.slide_layout,container,false);

        ImageView slideImageView = (ImageView) view.findViewById((R.id.slide_image));


//        slideImageView.setImageResource(slide_images[position]);
        Log.e("Worck",position+"");
        slideImageView.setImageBitmap(bitmaps.get(position));

        container.addView(view);
        return  view;
    }
    @Override
    public void destroyItem(@NonNull ViewGroup contaner, int position, @NonNull Object object)
    {
        contaner.removeView((RelativeLayout)object);
    }

}
