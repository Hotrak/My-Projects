package mbab.iso.com.materialbottomappbar;


import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;

import androidx.annotation.RequiresApi;
import androidx.fragment.app.Fragment;

import android.os.StrictMode;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.BounceInterpolator;
import android.widget.AdapterView;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.ProgressBar;

import com.baoyz.swipemenulistview.SwipeMenu;
import com.baoyz.swipemenulistview.SwipeMenuCreator;
import com.baoyz.swipemenulistview.SwipeMenuItem;
import com.baoyz.swipemenulistview.SwipeMenuListView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;


/**
 * A simple {@link Fragment} subclass.
 */
public class BlankFragment extends Fragment {


    public BlankFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment

        return inflater.inflate(R.layout.fragment_blank, container, false);
    }

    String idUser;
    ProgressBar progressBar;
    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        idUser = ((GlopalParams) getActivity().getApplication()).getId();
        //progressBar = (ProgressBar) getView().findViewById(R.id.progressBar);


    }
    ArrayList<String> title;
    ArrayList<String> shortDisrip;
    ArrayList<String> imagepath;
    ArrayList<String> idDeliver;
    ArrayList<String> idDesire;
    ArrayList<String> stateTypes;
    ArrayList<Bitmap> bitmaps;

    String result;

    Bitmap emptiImage;
    SwipeMenuListView listView;
    public void collectData(Bitmap emptiImage, String result) {


        listView = (SwipeMenuListView) getView().findViewById(R.id.listView);
        this.emptiImage = emptiImage;
        JSONArray ja = null;
        JSONObject jo;
        try {
            ja = new JSONArray(result);
            jo = null;
            title = new ArrayList<String>();
            shortDisrip = new ArrayList<String>();
            imagepath = new ArrayList<String>();
            idDeliver = new ArrayList<String>();
            idDesire = new ArrayList<String>();
            bitmaps = new ArrayList<Bitmap>();
            stateTypes = new ArrayList<String>();


        } catch (JSONException e) {
            Log.e("Image:!!!", "EROR 33");
            Log.e("Image:!!!", e.getMessage());
            listView.setVisibility(View.GONE);
            LinearLayout linearLayout = (LinearLayout) getView().findViewById(R.id.not_find_error);
            linearLayout.setVisibility(View.VISIBLE);
            return;
        }
        for (int i = 0; i <= ja.length(); i++) {

            try {
                jo = ja.getJSONObject(i);
                title.add(jo.getString("human_settlements"));
                shortDisrip.add("г. " + jo.getString("human_settlements") + ", ул. " + jo.getString("street"));//
                imagepath.add(getPhoto(jo.getString("photo"), 1));
                idDeliver.add(jo.getString("id_realtys"));
                idDesire.add(jo.getString("id_desire"));
                stateTypes.add("1");


                URL url = new URL(imagepath.get(i));
                Bitmap bmp = BitmapFactory.decodeStream(url.openConnection().getInputStream());
                bitmaps.add(bmp);
            } catch (JSONException e) {
                Log.e("TEST1", "1111");
            } catch (MalformedURLException e) {
                Log.e("TEST1", "2222");
            } catch (IOException e) {
                bitmaps.add(emptiImage);
                Log.e("TEST1", "3333");
            }

        }

        setSwipeAdapter();

    }

    private void setSwipeAdapter() {


        myAdsAdapter customListView = new myAdsAdapter(getActivity(), title, shortDisrip, bitmaps, stateTypes);
        listView.setAdapter(customListView);

        SwipeMenuCreator creator = new SwipeMenuCreator() {

            @Override
            public void create(SwipeMenu menu) {


                createItemDelete(menu);

            }
        };
        listView.setOnMenuItemClickListener(new SwipeMenuListView.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(int position, SwipeMenu menu, int index) {
                LinearLayout linearLayout = (LinearLayout) listView.getChildAt(position).findViewById(R.id.textLayout);


                switch (index) {
                    case 0:

                        Delete(idDesire.get(position),idUser);
                        break;

                }

                // false : close the menu; true : not close the menu
                return false;
            }
        });


        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Intent questionIntent = new Intent(getContext(), AboutActivity.class);
                questionIntent.putExtra("ID", idDeliver.get(position));
                startActivity(questionIntent);
            }
        });

// set creator
        listView.setMenuCreator(creator);
        listView.setCloseInterpolator(new BounceInterpolator());
    }

    public String getPhoto(String str, int num) {
        JSONArray ja;
        JSONObject jo;

        jo = null;
        String result = "none.jpg";
        try {

            jo = new JSONObject(str);

            jo = jo.getJSONObject(num + "");

            if (jo.getString("photo") != "")
                result = jo.getString("photo");
            else
                return "http://bomj.malaha.beget.tech/img/none.jpg";

        } catch (JSONException e) {
            Log.e("IMAGE !!!098:", e.getMessage());
            return "http://bomj.malaha.beget.tech/img/none.jpg";
        }
        return "http://bomj.malaha.beget.tech/img/" + result;

        //return "http://bomj.malaha.beget.tech/img/none.jpg";
    }


    public void Delete(String id,String idUsers)
    {
        StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
        BlankFragment.ExampleAsyncTask exampleAsyncTask = new BlankFragment.ExampleAsyncTask();
        String query1 ="SELECT b.region AS region ,human_settlements,street,photo,a.id_realtys,c.id_desire FROM realtys a, regions b,desires c WHERE a.id_region = b.id_region and c.id_realtys = a.id_realtys and c.id = "+idUsers;
        exampleAsyncTask.execute(query1+" <>  DELETE FROM desires WHERE id_desire =  "+ id,"http://bomj.malaha.beget.tech/Home/UploadFile.php");

    }


    private void createItemDelete(SwipeMenu menu) {
        SwipeMenuItem deleteItem = new SwipeMenuItem(
                getContext());
        deleteItem.setBackground(new ColorDrawable(Color.rgb(0xAD,
                0x1f, 0x1f)));
//            deleteItem.setBackground(new ColorDrawable(Color.rgb(0x22,
//                    0x1d, 0x44)));
        deleteItem.setWidth(170);
        deleteItem.setIcon(R.drawable.ic_delete_black_24dp);
        menu.addMenuItem(deleteItem);
    }

    private class ExampleAsyncTask extends AsyncTask<String, String, String> {


        public ExampleAsyncTask() {

        }

        boolean onliQwier = false;



        public ExampleAsyncTask(boolean onliQwier) {
            this.onliQwier = onliQwier;
        }

        @Override
        protected void onPreExecute() {
            super.onPreExecute();

            //progressBar.setVisibility(View.VISIBLE);
        }

        @Override
        protected String doInBackground(String... strs) {
            URL url1 = null;
            try {
                url1 = new URL("http://bomj.malaha.beget.tech/img/none.jpg");
                emptiImage = BitmapFactory.decodeStream(url1.openConnection().getInputStream());

            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }


            zapros Zaprps = new zapros(strs[1]);

            Zaprps.start(strs[0]);
            try {
                Zaprps.join();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }


            result = Zaprps.result;


            return "Finished!";
        }

        @RequiresApi(api = Build.VERSION_CODES.M)
        @Override
        protected void onPostExecute(String s) {
            super.onPostExecute(s);

            //progressBar.setVisibility(View.GONE);

            Log.e("RESULT_ADS: ", result.trim());
            if (!onliQwier)
                collectData(emptiImage, result);

        }
    }

}
