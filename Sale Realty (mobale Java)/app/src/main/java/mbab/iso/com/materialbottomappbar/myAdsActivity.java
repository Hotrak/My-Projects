package mbab.iso.com.materialbottomappbar;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;

import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.BounceInterpolator;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

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

public class myAdsActivity extends AppCompatActivity {

    Toolbar tolBar;
    RelativeLayout progressBar;
    String result;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_my_ads);


        progressBar = findViewById(R.id.progressBar);
        StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
        myAdsActivity.ExampleAsyncTask exampleAsyncTask = new myAdsActivity.ExampleAsyncTask();
        exampleAsyncTask.execute("SELECT  state,b.region AS region ,human_settlements,street,photo,id_realtys,c.duration FROM realtys a, regions b,durations c WHERE c.id_duration = a.id_duration and  a.id_region = b.id_region and id = "+((GlopalParams) getApplication()).getId(),"http://bomj.malaha.beget.tech/Home/Load.php");


        tolBar= findViewById(R.id.toolbar);
        tolBar.setNavigationOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                finish();

            }
        });

        final SwipeRefreshLayout mSwipeRefreshLayout;

        mSwipeRefreshLayout = (SwipeRefreshLayout) findViewById(R.id.swipeRefresh);
        mSwipeRefreshLayout.setColorSchemeColors(
                  Color.RED, Color.GREEN, Color.BLUE, Color.CYAN);

        mSwipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                Toast.makeText(getApplication(),"UPDATE",Toast.LENGTH_LONG).show();
                mSwipeRefreshLayout.setRefreshing(false);
                Upload();
            }
        });


    }

    private void Upload()
    {
        StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
        myAdsActivity.ExampleAsyncTask exampleAsyncTask = new myAdsActivity.ExampleAsyncTask();
        exampleAsyncTask.execute("SELECT  state,b.region AS region ,human_settlements,street,photo,id_realtys,c.duration FROM realtys a, regions b,durations c WHERE c.id_duration = a.id_duration and  a.id_region = b.id_region and id = "+((GlopalParams) getApplication()).getId(),"http://bomj.malaha.beget.tech/Home/Load.php");

    }
    ArrayList<String> title;
    ArrayList<String> shortDisrip;
    ArrayList<String> imagepath;
    ArrayList<String> idDeliver;
    ArrayList<String> stateTypes;
    ArrayList<Bitmap> bitmaps;
    public void collectData(Bitmap emptiImage, String result) {


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
            bitmaps = new ArrayList<Bitmap>();
            stateTypes = new ArrayList<String>();


        } catch (JSONException e) {
            Log.e("Image:!!!", "EROR 33");
            Log.e("Image:!!!", e.getMessage());
        }
        for (int i = 0; i <= ja.length(); i++) {

            try {
                jo = ja.getJSONObject(i);
                title.add(jo.getString("duration"));
                shortDisrip.add("г. " + jo.getString("human_settlements") + ", ул. " + jo.getString("street"));//
                imagepath.add(getPhoto(jo.getString("photo"), 1));
                idDeliver.add(jo.getString( "id_realtys"));
                stateTypes.add(jo.getString("state"));


                URL url = new URL(imagepath.get(i));
                Bitmap bmp = BitmapFactory.decodeStream(url.openConnection().getInputStream());
                bitmaps.add(bmp);
            } catch (JSONException e) {
                Log.e("TEST1", "1111");
                //return;
            } catch (MalformedURLException e) {
                Log.e("TEST1", "2222");
            } catch (IOException e) {
                bitmaps.add(emptiImage);
                Log.e("TEST1", "3333");
            }

        }

        setSwipeAdapter();

    }
    private void setSwipeAdapter()
    {
        final SwipeMenuListView listView = findViewById(R.id.listView);

        myAdsAdapter customListView=new myAdsAdapter(this,title,shortDisrip,bitmaps,stateTypes);
        listView.setAdapter(customListView);

        SwipeMenuCreator creator = new SwipeMenuCreator() {

            @Override
            public void create(SwipeMenu menu) {

                switch(menu.getViewType()) {
                    case 0:
                        createItemClock(menu);

                        break;
                    case 1:
                        createItemHide(menu);

                        break;
                    case 2:
                        createItemAtive(menu);

                        break;
                }
                createItemEdit(menu);
                createItemDelete(menu);

            }
        };
        listView.setOnMenuItemClickListener(new SwipeMenuListView.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(int position, SwipeMenu menu, int index) {
                LinearLayout linearLayout = (LinearLayout) listView.getChildAt(position).findViewById(R.id.textLayout);
                setMargins(linearLayout,0,0,0,0);

                switch (index) {
                    case 0:
                        if(stateTypes.get(position).equals("2"))
                            ChengeState(position,"3");
                        if(stateTypes.get(position).equals("3"))
                            ChengeState(position,"2");
                        else
                            Toast.makeText(getApplicationContext(),"Находится на рассмотрениии",Toast.LENGTH_LONG).show();
                        break;
                    case 1:
                        Toast.makeText(getApplicationContext(),"1",Toast.LENGTH_LONG).show();
                        break;
                    case 2:

                        Delete(position);
                        Toast.makeText(getApplicationContext(),"2",Toast.LENGTH_LONG).show();

                        break;
                }

                // false : close the menu; true : not close the menu
                return false;
            }
        });

        listView.setOnMenuStateChangeListener(new SwipeMenuListView.OnMenuStateChangeListener() {
            @Override
            public void onMenuOpen(int position) {
                LinearLayout linearLayout = (LinearLayout) listView.getChildAt(position).findViewById(R.id.textLayout);
                setMargins(linearLayout,180,0,0,0);
            }

            @Override
            public void onMenuClose(int position) {
                LinearLayout linearLayout = (LinearLayout) listView.getChildAt(position).findViewById(R.id.textLayout);
                setMargins(linearLayout,0,0,0,0);
            }
        });
        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Intent questionIntent = new Intent(getApplicationContext(), AboutActivity.class);
                questionIntent.putExtra("ID",idDeliver.get(position));
                startActivity(questionIntent);
            }
        });

// set creator
        listView.setMenuCreator(creator);
        listView.setCloseInterpolator(new BounceInterpolator());
    }
    public static void setMargins (View v, int l, int t, int r, int b) {
        if (v.getLayoutParams() instanceof ViewGroup.MarginLayoutParams) {
            ViewGroup.MarginLayoutParams p = (ViewGroup.MarginLayoutParams) v.getLayoutParams();
            p.setMargins(l, t, r, b);
            v.requestLayout();
        }
    }

    private void createItemHide(SwipeMenu menu) {
        SwipeMenuItem frizItem = new SwipeMenuItem(
                getApplicationContext());
        frizItem.setBackground(new ColorDrawable(Color.rgb(0x22,
                0x1d, 0x44)));
        frizItem.setWidth(170);
        frizItem.setIcon(R.drawable.ic_hide);
        menu.addMenuItem(frizItem);
    }
    private void createItemAtive(SwipeMenu menu) {
        SwipeMenuItem frizItem = new SwipeMenuItem(
                getApplicationContext());
        frizItem.setBackground(new ColorDrawable(Color.rgb(0x22,
                0x1d, 0x44)));
        frizItem.setWidth(170);
        frizItem.setIcon(R.drawable.ic_view);
        menu.addMenuItem(frizItem);
    }
    private void createItemClock(SwipeMenu menu) {
        SwipeMenuItem frizItem = new SwipeMenuItem(
                getApplicationContext());
        frizItem.setBackground(new ColorDrawable(Color.rgb(0x22,
                0x1d, 0x44)));
        frizItem.setWidth(170);
        frizItem.setIcon(R.drawable.clock);
        menu.addMenuItem(frizItem);
    }

    private void createItemEdit(SwipeMenu menu) {
        SwipeMenuItem editItem= new SwipeMenuItem(
                getApplicationContext());
        editItem.setBackground(new ColorDrawable(Color.rgb(0x22,
                0x1d, 0x44)));
        editItem.setWidth(170);
        editItem.setIcon(R.drawable.ic_pencil);
        menu.addMenuItem(editItem);
    }

    private void createItemDelete(SwipeMenu menu) {
        SwipeMenuItem deleteItem = new SwipeMenuItem(
                getApplicationContext());
//        deleteItem.setBackground(new ColorDrawable(Color.rgb(0xAD,
//                0x1f, 0x1f)));
        deleteItem.setBackground(new ColorDrawable(Color.rgb(0x22,
                0x1d, 0x44)));
        deleteItem.setWidth(170);
        deleteItem.setIcon(R.drawable.ic_delete_black_24dp);
        menu.addMenuItem(deleteItem);
    }
    public String getPhoto(String str,int num )
    {
        JSONArray ja;
        JSONObject jo;

        jo = null;
        String result = "none.jpg";
        try {

            jo = new JSONObject(str);

            jo = jo.getJSONObject(num+"");

            if(jo.getString("photo")!="")
                result = jo.getString("photo");
            else
                return "http://bomj.malaha.beget.tech/img/none.jpg";

        } catch (JSONException e) {
            Log.e("IMAGE !!!098:",e.getMessage());
            return "http://bomj.malaha.beget.tech/img/none.jpg";
        }
        return "http://bomj.malaha.beget.tech/img/"+result;

        //return "http://bomj.malaha.beget.tech/img/none.jpg";
    }
    public void ChengeState(int position,String state)
    {
        String query = "SELECT  state,b.region AS region ,human_settlements,street,photo,id_realtys FROM realtys a, regions b WHERE a.id_region = b.id_region and id = "+((GlopalParams) getApplication()).getId()+" <> UPDATE realtys SET state = "+ state+" WHERE id_realtys = "+ idDeliver.get(position)+" ;";//DELETE FROM comments WHERE id_realtys= "+ idDeliver.get(position)+";
        StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
        myAdsActivity.ExampleAsyncTask exampleAsyncTask = new myAdsActivity.ExampleAsyncTask();
        exampleAsyncTask.execute(query,"http://bomj.malaha.beget.tech/Home/UploadFile.php");

    }
    public void Delete(int position)
    {

        String query = idDeliver.get(position);
        StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
        myAdsActivity.ExampleAsyncTask exampleAsyncTask = new myAdsActivity.ExampleAsyncTask();
        exampleAsyncTask.execute("SELECT  state,b.region AS region ,human_settlements,street,photo,id_realtys FROM realtys a, regions b WHERE a.id_region = b.id_region and id = "+((GlopalParams) getApplication()).getId()+" <> "+query,"http://bomj.malaha.beget.tech/Home/deleteFile.php");
    }

    Bitmap emptiImage;
    private class ExampleAsyncTask extends AsyncTask<String, String, String> {


        public ExampleAsyncTask()
        {

        }
        boolean onliQwier = false;

        public ExampleAsyncTask(boolean onliQwier)
        {
            this.onliQwier = onliQwier;
        }
        @Override
        protected void onPreExecute() {
            super.onPreExecute();

            progressBar.setVisibility(View.VISIBLE);
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

            progressBar.setVisibility(View.GONE);

            Log.e("RESULT_ADS: ",result.trim());
            if(!onliQwier)
                collectData(emptiImage,result);

        }
    }
}
