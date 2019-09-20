package mbab.iso.com.materialbottomappbar;



import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.media.Image;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;

import android.os.StrictMode;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.RatingBar;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.widget.NestedScrollView;
import androidx.viewpager.widget.ViewPager;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.lang.ref.WeakReference;
import java.net.MalformedURLException;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class AboutActivity extends AppCompatActivity  {

    private ViewPager mSlideViewPager;
    private LinearLayout mDotLayout;
    private Toolbar toolbar;
    private SliderAdapter sliderAdapter;

    String[] name;
    String[] email;
    String[] imagepath;
    ListView listView;


    private TextView timeAdd;
    private TextView prace;

    private TextView oblst;
    private TextView regionView;
    private TextView countRum;
    private TextView ataj;
    private TextView washroom;


    private TextView sqereBase;
    private TextView sqereLive;
    private TextView sqereKyk;

    private ImageView mebImg;
    private ImageView mebKykImg;
    private ImageView holodImg;
    private ImageView woshMashImg;
    private ImageView internetImg;
    private ImageView kondicImg;
    private ImageView balcon;

    private TextView type;
    private TextView typeHome;
    private TextView typePlanir;
    private TextView typeRem;

    private TextView dataBild;

    private TextView shortDiscription;
    private TextView longDiscription;

    private int doneImage = R.drawable.ic_done_black_24dp;
    private int unDoneImage = R.drawable.ic_close_black_24dp;

    private ArrayList<Bitmap> bitmaps;

    private ArrayList<String> logins;
    private ArrayList<String> coments;
    private ArrayList<String> idCommentsUsers;
    private boolean[] isAMan;

    RatingBar reeting1;
    RatingBar reeting2;
    RatingBar reeting3;
    String id;
    int possition;

    public RelativeLayout progressBar;

    ImageView addLike;

    ArrayList<String> idHels;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_about);

        addLike = (ImageView) findViewById(R.id.addLike);
        //getWindow().setFlags(WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS,WindowManager.LayoutParams.N);
//        TextView textView = (TextView)findViewById(R.id.textView);

        //textView.setText(massage);

        possition = -1;
        id = getIntent().getSerializableExtra("ID").toString();
        if(getIntent().getSerializableExtra("POS")!=null)
        {
            possition = Integer.parseInt(getIntent().getSerializableExtra("POS").toString());
            idHels = ((GlopalParams) this.getApplication()).getHels();

        }

        try {
            if(possition!=-1)
                if(Integer.parseInt(idHels.get(possition))>0 )
                    ChengeHelsIcone(addLike);
                else
                    addLike.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            String query = "INSERT INTO desires (id_realtys,id) VALUES ("+id+","+((GlopalParams) getApplication()).getId()+")";
                            AboutActivity.ExampleAsyncTask task2 = new AboutActivity.ExampleAsyncTask(3);
                            task2.execute(query,"http://bomj.malaha.beget.tech/Home/Load.php");
                            ChengeHelsIcone(addLike);
                            if(idHels!=null)
                                idHels.set(possition,"1");
                            saveHels();
                        }
                    });
            else
                ChengeHelsIcone(addLike);
        }catch (Exception e)
        {

        }


        progressBar = findViewById(R.id.progressBar);


        RelativeLayout callButton = findViewById(R.id.callButton);

        callButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent callIntent = new Intent(Intent.ACTION_DIAL);
                callIntent.setData(Uri.parse("tel:"+phone));
                startActivity(callIntent);
            }
        });

        Toolbar toolbar = findViewById(R.id.toolbar);
        toolbar.setNavigationOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });




    }
    RatingBar reeting1User;
    RatingBar reeting2User;
    RatingBar reeting3User;

    private void saveHels()
    {
        ((GlopalParams) this.getApplication()).setHels(idHels);

    }
    @Override
    public void onResume() {
        super.onResume();

        timeAdd = findViewById(R.id.timeAdd);
        prace= findViewById(R.id.price);

        oblst= findViewById(R.id.olast);
        regionView= findViewById(R.id.reg);
        countRum= findViewById(R.id.countRum);
        ataj= findViewById(R.id.ataj);

        sqereBase= findViewById(R.id.sOb);
        sqereLive= findViewById(R.id.sJil);
        sqereKyk= findViewById(R.id.sKyk);

        type= findViewById(R.id.type);
        typeHome= findViewById(R.id.typeHome);
        typePlanir= findViewById(R.id.typePlan);
        typeRem= findViewById(R.id.typeRem);

        mebImg= findViewById(R.id.mebelImg);
        mebKykImg= findViewById(R.id.kykMImg);
        holodImg= findViewById(R.id.holodilnicImg);
        woshMashImg= findViewById(R.id.woshMashinImg);
        internetImg= findViewById(R.id.internetImg);
        kondicImg= findViewById(R.id.kondicImg);
        balcon= findViewById(R.id.balcon);

        dataBild = findViewById(R.id.god);
        longDiscription = findViewById(R.id.discription);

        washroom = findViewById(R.id.sanyzel);

        reeting1 = findViewById(R.id.infrpStr1);
        reeting2 = findViewById(R.id.infrpStr2);
        reeting3 = findViewById(R.id.infrpStr3);

//liadBar.setVisibility(View.GONE);

        //1 String query ="SELECT users.phone, durations.duration, type_realtys.type_realty, type_houses.type_house, type_upkeeps.type_upkeep, type_layouts.type_layout, washrooms.washroom, areas.area, regions.region, realtys.human_settlements, realtys.street, realtys.house_number, realtys.full_area, realtys.living_area, realtys.kitchen_area, realtys.floor, realtys.count_floor, realtys.room, realtys.photo, realtys.description, realtys.cost, realtys.furniture, realtys.kitchen_furniture, realtys.refrigerator, realtys.washing_machine, realtys.Internet, realtys.loggia_or_balcony, realtys.air_conditioning, realtys.date_added, realtys.date_change, realtys.date_build, realtys.location_x, realtys.location_y, realtys.views, realtys.state FROM realtys INNER JOIN type_realtys ON type_realtys.id_type_realty = realtys.id_type_realty INNER JOIN type_upkeeps ON type_upkeeps.id_type_upkeep = realtys.id_type_upkeep INNER JOIN type_layouts ON type_layouts.id_type_layout = realtys.id_type_layout INNER JOIN type_houses ON type_houses.id_type_house = realtys.id_type_house INNER JOIN durations ON durations.id_duration = realtys.id_duration INNER JOIN washrooms ON washrooms.id_washroom = realtys.id_washroom INNER JOIN users ON users.id = realtys.id INNER JOIN areas ON areas.id_area = realtys.id_area INNER JOIN regions ON regions.id_region = realtys.id_region INNER JOIN users ON users.id = realtys.id WHERE realtys.id_realtys = "+id+"";
        String query ="SELECT users.phone, durations.duration, type_realtys.type_realty, type_houses.type_house, type_upkeeps.type_upkeep, type_layouts.type_layout, washrooms.washroom, areas.area, regions.region, realtys.human_settlements, realtys.street, realtys.house_number, realtys.full_area, realtys.living_area, realtys.kitchen_area, realtys.floor, realtys.count_floor, realtys.room, realtys.photo, realtys.description, realtys.cost, realtys.furniture, realtys.kitchen_furniture, realtys.refrigerator, realtys.washing_machine, realtys.Internet, realtys.loggia_or_balcony, realtys.air_conditioning, realtys.date_added, realtys.date_change, realtys.date_build, realtys.location_x, realtys.location_y, realtys.views, realtys.state FROM realtys INNER JOIN type_realtys ON type_realtys.id_type_realty = realtys.id_type_realty INNER JOIN type_upkeeps ON type_upkeeps.id_type_upkeep = realtys.id_type_upkeep INNER JOIN type_layouts ON type_layouts.id_type_layout = realtys.id_type_layout INNER JOIN type_houses ON type_houses.id_type_house = realtys.id_type_house INNER JOIN durations ON durations.id_duration = realtys.id_duration INNER JOIN washrooms ON washrooms.id_washroom = realtys.id_washroom INNER JOIN users ON users.id = realtys.id INNER JOIN areas ON areas.id_area = realtys.id_area INNER JOIN regions ON regions.id_region = realtys.id_region WHERE realtys.id_realtys = "+id+"";
        //2        String query = "SELECT washrooms.washroom, type_upkeeps.type_upkeep, regions.region, type_layouts.type_layout, type_houses.type_house, durations.duration, type_realtys.type_realty, areas.area, realtys.human_settlements, realtys.street, realtys.house_number, realtys.full_area, realtys.living_area, realtys.kitchen_area, realtys.floor, realtys.count_floor, realtys.room, realtys.photo, realtys.description, realtys.cost, realtys.state, realtys.furniture, realtys.kitchen_furniture, realtys.refrigerator, realtys.washing_machine, realtys.Internet, realtys.loggia_or_balcony, realtys.air_conditioning, realtys.date_added, realtys.date_change, realtys.date_build, realtys.location_x, realtys.location_y, realtys.views, realtys.id_realtys, durations.id_duration, type_layouts.id_type_layout, type_houses.id_type_house, type_realtys.id_type_realty, areas.id_area, washrooms.washroom, regions.id_area, regions.id_region, type_upkeeps.id_type_upkeep FROM type_realtys INNER JOIN realtys ON type_realtys.id_type_realty = realtys.id_type_realty INNER JOIN areas ON areas.id_area = realtys.id_area INNER JOIN regions ON regions.id_area = realtys.id_area AND regions.id_region = realtys.id_region INNER JOIN type_upkeeps ON type_upkeeps.id_type_upkeep = realtys.id_type_upkeep INNER JOIN type_houses ON type_houses.id_type_house = realtys.id_type_house INNER JOIN type_layouts ON type_layouts.id_type_layout = realtys.id_type_layout INNER JOIN washrooms ON washrooms.id_washroom = realtys.id_washroom INNER JOIN durations ON durations.id_duration = realtys.id_duration WHERE realtys.id_realtys = "+id;
        AboutActivity.ExampleAsyncTask task = new AboutActivity.ExampleAsyncTask(1);
        task.execute(query,"http://bomj.malaha.beget.tech/Home/Load.php");


        reeting1User = findViewById(R.id.UserInfrpStr1);
        reeting2User = findViewById(R.id.UserInfrpStr2);
        reeting3User = findViewById(R.id.UserInfrpStr3);
        //LoadData(id);

        comment = ((EditText)findViewById(R.id.comment));
        button = (Button) findViewById(R.id.commentButton);

        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {



//                String query2 = "SELECT a.comment,a.date_added,b.name,AVG(a.infrastructure) as inf,AVG(a.layout) as lay, AVG(a.neighbourhood) as nei FROM rating_realtys a, users b WHERE a.id = b.id and a.id_realty = "+id;
                String query2 = "SELECT a.comment,a.date_added,b.name,AVG(a.infrastructure) as inf,AVG(a.layout) as lay, AVG(a.neighbourhood) as nei,a.id FROM rating_realtys a, users b WHERE a.id = b.id and a.id_realty = '"+id+"' GROUP BY a.comment,a.date_added,b.name,a.id_realty";

                String query = query2+ " <> INSERT INTO rating_realtys (id_realty,id,infrastructure,layout,neighbourhood,comment,state) VALUES ('"+id+"', '"+((GlopalParams) getApplication()).getId()+"','"+reeting1User.getRating()+"','"+reeting2User.getRating()+"','"+reeting3User.getRating()+"','"+comment.getText()+"','1')";//((GlopalParams) getApplication()).getId()
                StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
                AboutActivity.ExampleAsyncTask exampleAsyncTask = new AboutActivity.ExampleAsyncTask(2);
                exampleAsyncTask.execute(query,"http://bomj.malaha.beget.tech/Home/UploadFile.php");
            }
        });

    }
    boolean isHave;
    public String result;
    String phone;

    EditText comment;
    Button button;
    public void LoadData(String result)
    {

//JSON
        JSONArray ja = null;
        JSONObject jo = null;

        Log.e("ERROR_LOAD",result);
        try {
            ja = new JSONArray(result);
            jo = ja.getJSONObject(0);


            timeAdd.setText(jo.getString("date_added"));
            prace.setText(jo.getString("cost")+ " p.");

            regionView.setText(jo.getString("region"));
            oblst.setText(jo.getString("area"));



            if(!jo.getString("floor").equals("0"))
                countRum.setText(jo.getString("room"));
            else
            {
                LinearLayout layout = (LinearLayout) findViewById(R.id.countRumLay);
                layout.setVisibility(View.GONE);
            }


            if(!jo.getString("floor").equals("0"))
                ataj.setText(jo.getString("floor"));
            else
            {
                LinearLayout layout = (LinearLayout) findViewById(R.id.atajLay);
                layout.setVisibility(View.GONE);
            }


            if(!jo.getString("washroom").equals("Неважно"))
                washroom.setText(jo.getString("washroom"));
            else{
                LinearLayout layout = (LinearLayout) findViewById(R.id.sanyzelLay);
                layout.setVisibility(View.GONE);
            }

            if(!jo.getString("full_area").equals("0"))
                sqereBase.setText(jo.getString("full_area"));
            else{
                LinearLayout layout = (LinearLayout) findViewById(R.id.sObLay);
                layout.setVisibility(View.GONE);
            }


            if(!jo.getString("living_area").equals("0"))
                sqereBase.setText(jo.getString("living_area"));
            else{
                LinearLayout layout = (LinearLayout) findViewById(R.id.sJilLay);
                layout.setVisibility(View.GONE);
            }

            if(!jo.getString("kitchen_area").equals("0"))
                sqereKyk.setText(jo.getString("kitchen_area"));
            else{
                LinearLayout layout = (LinearLayout) findViewById(R.id.sKykLay);
                layout.setVisibility(View.GONE);
            }

            if(!jo.getString("type_realty").equals("Неважно"))
                type.setText(jo.getString("type_realty"));
            else{
                LinearLayout layout = (LinearLayout) findViewById(R.id.typeLay);
                layout.setVisibility(View.GONE);
            }

            if(!jo.getString("type_house").equals("Неважно"))
                typeHome.setText(jo.getString("type_house"));
            else{
                LinearLayout layout = (LinearLayout) findViewById(R.id.typeHomeLayLay);
                layout.setVisibility(View.GONE);
            }
            typePlanir.setText(jo.getString("type_layout"));
            if(!jo.getString("type_layout").equals("Неважно"))
                typePlanir.setText(jo.getString("type_layout"));
            else{
                LinearLayout layout = (LinearLayout) findViewById(R.id.typePlanLay);
                layout.setVisibility(View.GONE);
            }


            if(!jo.getString("type_upkeep").equals("Неважно"))
                typeRem.setText(jo.getString("type_upkeep"));
            else{
                LinearLayout layout = (LinearLayout) findViewById(R.id.typeRemLay);
                layout.setVisibility(View.GONE);
            }

            if(!jo.getString("date_build").equals("0"))
                dataBild.setText(jo.getString("date_build"));
            else{
                LinearLayout layout = (LinearLayout) findViewById(R.id.godLay);
                layout.setVisibility(View.GONE);
            }

            longDiscription.setText(jo.getString("description"));

            holodImg.setImageResource(jo.getInt("refrigerator")==0 ? unDoneImage : doneImage);
            mebImg.setImageResource(jo.getInt("kitchen_furniture")==0 ? unDoneImage : doneImage);
            mebKykImg.setImageResource(jo.getInt("furniture")==0 ? unDoneImage : doneImage);
            kondicImg.setImageResource(jo.getInt("air_conditioning")==0 ? unDoneImage : doneImage);
            internetImg.setImageResource(jo.getInt("Internet")==0 ? unDoneImage : doneImage);
            woshMashImg.setImageResource(jo.getInt("washing_machine")==0 ? unDoneImage : doneImage);
            balcon.setImageResource(jo.getInt("loggia_or_balcony")==0 ? unDoneImage : doneImage);


            phone = jo.getString("phone");
            bitmaps = new ArrayList<Bitmap>();




        } catch (JSONException e) {
            Log.e("URLIMAGE","Lol4");
        }
        for (int i = 1; i<9 ;i++)
        {
            URL url = null;
            try {
                url = new URL(getPhoto(jo.getString("photo"),i));
                bitmaps.add(BitmapFactory.decodeStream(url.openConnection().getInputStream()));
                isHave = true;

            } catch (MalformedURLException e) {
                Log.e("URLIMAGE","Lol1");
            } catch (JSONException e) {
                Log.e("URLIMAGE","Lol2");

            } catch (IOException e) {
                Log.e("URLIMAGE","Lol3");
            }catch (NullPointerException e) {
                Log.e("URLIMAGE","Lol4");
            }



        }
        if(!isHave)
        {
            URL url = null;
            try {
                bitmaps = new ArrayList<Bitmap>();
                    url = new URL("http://bomj.malaha.beget.tech/img/none.jpg");
                bitmaps.add(BitmapFactory.decodeStream(url.openConnection().getInputStream()));
            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }

        }
        if(bitmaps!=null)
        {
            mSlideViewPager = (ViewPager) findViewById(R.id.slideViewPage);
            sliderAdapter = new SliderAdapter(AboutActivity.this,bitmaps);
            mSlideViewPager.setAdapter(sliderAdapter);
        }

        //Log.e("SIZE:",bitmaps.size()+"");
        String query = "SELECT a.comment,a.date_added,b.name,AVG(a.infrastructure) as inf,AVG(a.layout) as lay, AVG(a.neighbourhood) as nei,a.id FROM rating_realtys a, users b WHERE a.id = b.id and a.id_realty = '"+id+"' GROUP BY a.comment,a.date_added,b.name,a.id_realty";
        AboutActivity.ExampleAsyncTask task2 = new AboutActivity.ExampleAsyncTask(2);
        task2.execute(query,"http://bomj.malaha.beget.tech/Home/Load.php");

    }
    private void ChengeHelsIcone(ImageView holodImg)
    {
        holodImg.setImageResource(R.drawable.ic_favoriteheartbutton);
    }
    private void LoadComents(String result) {

        JSONArray ja = null;
        JSONObject jo = null;

        try {
            ja = new JSONArray(result);
            logins = new ArrayList<>();
            coments = new ArrayList<>();
            idCommentsUsers = new ArrayList<>();
            Log.e("COMENT_SIZE", ja.length()+"");

            double inf=0;
            double lay=0;
            double nei=0;
            int id = -1;
            try {
                id = Integer.parseInt (((GlopalParams) getApplication()).getId());
            }catch (Exception e)
            {
                reeting1User.setIsIndicator(true);
                reeting2User.setIsIndicator(true);
                reeting3User.setIsIndicator(true);

                comment.setVisibility(View.GONE);
                button.setVisibility(View.GONE);
                addLike.setVisibility(View.GONE);


            }


            for(int i = 0;i< ja.length();i++)
            {
                jo = ja.getJSONObject(i);

                logins.add(jo.getString("name"));
                coments.add(jo.getString("comment"));
                idCommentsUsers.add(jo.getString("id"));

                inf += jo.getInt("inf");
                lay += jo.getInt("lay");
                nei += jo.getInt("nei");

                if(id!=-1)
                    if(Integer.parseInt(idCommentsUsers.get(i))== id)
                    {
                        reeting1User.setRating(jo.getInt("inf"));
                        reeting2User.setRating(jo.getInt("lay"));
                        reeting3User.setRating(jo.getInt("nei"));

                        reeting1User.setIsIndicator(true);
                        reeting2User.setIsIndicator(true);
                        reeting3User.setIsIndicator(true);
                    }
            }



            double avg = (inf + lay + nei)/ja.length()/3;

            ProgressBar progressBar = (ProgressBar) findViewById(R.id.PROGRESS_BAR);
            progressBar.setProgress((int) (avg*2*10));
            TextView progressText = (TextView) findViewById(R.id.PROGRESS_TEXT);
            double vel = (double) Math.round(avg*10)/10;
            progressText.setText( vel+"");

            reeting1.setRating((float) inf/ja.length());
            reeting2.setRating((float) lay/ja.length());
            reeting3.setRating((float) nei/ja.length());


            Log.e("INF",inf+"");
            for(int i = 0; i< idCommentsUsers.size();i++)
            {
                if(Integer.parseInt(idCommentsUsers.get(i))== id)
                {
                    comment.setVisibility(View.GONE);
                    button.setVisibility(View.GONE);

                    break;
                }
            }

            listView=(ListView)findViewById(R.id.comentsListView);
            StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
            CustomListViewComents customListView=new CustomListViewComents(AboutActivity.this,logins,coments);
            listView.setAdapter(customListView);


        } catch (JSONException e) {
            LinearLayout linearLayout = findViewById(R.id.reatingLayout);
            linearLayout.setVisibility(View.GONE);
            Log.e("URLIMAGE","Lol4");
        }


    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater menuInflater = getMenuInflater();
        menuInflater.inflate(R.menu.test, menu);
        return true;
    }
    public String getPhoto(String str,int num )
    {
        JSONArray ja;
        JSONObject jo;
        jo = null;
        String result ="123";
        try {

            jo = new JSONObject(str);
            jo = jo.getJSONObject(num+"");
            result = jo.getString("photo");

        }catch (Exception e)
        {
            Log.d("IMAGE !!!2:",e.getMessage());
            return "http://bomj.malaha.beget.tech/img/none.jpg";

        }
        return "http://bomj.malaha.beget.tech/img/"+result;
 //       return "http://malaha.beget.tech/img/none.jpg";
    }



    private class ExampleAsyncTask extends AsyncTask<String, String, String> {


        int mes;

        public ExampleAsyncTask(int mes)
        {
            this.mes = mes;
        }
        @Override
        protected void onPreExecute() {
            super.onPreExecute();

            if(mes==1)
                progressBar.setVisibility(View.VISIBLE);

        }

        @Override
        protected String doInBackground(String... strs) {

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

        @Override
        protected void onPostExecute(String s) {
            super.onPostExecute(s);

            Log.e("ERROR_LOAD",result);

            progressBar.setVisibility(View.GONE);
            if(mes ==1)
                LoadData(result);
            if(mes == 2)
                LoadComents(result);


        }
    }



}

