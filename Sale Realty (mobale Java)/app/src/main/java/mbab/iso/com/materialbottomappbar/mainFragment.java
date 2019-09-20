package mbab.iso.com.materialbottomappbar;


import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
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
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.material.bottomappbar.BottomAppBar;
import com.google.android.material.floatingactionbutton.FloatingActionButton;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;


/**
 * A simple {@link Fragment} subclass.
 */
public class mainFragment extends Fragment {
    private BottomAppBar bar;
    private Switch aSwitch;
    private boolean fbModeCenter = true;
    private FloatingActionButton fab;

    //
    String urladdress = "malaha.beget.tech/Home/Load.php";
    ArrayList<String> price;
    ArrayList<String> shortDisrip;
    ArrayList<String> imagepath;
    ArrayList<Bitmap> bitmaps;
    ArrayList<String> idDeliver;
    ArrayList<String> idHels;

    ListView listView;
    BufferedInputStream is;
    String result;


    boolean isCreate  = false;
    public mainFragment() {


    }

    String[] data = {"Актуальные","По цене (По убыв.)", "По цене (По возр.)"};
    int nowPosition;
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_main, container, false);
    }
    LinearLayout liadBar;
    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);



        listView = (ListView) getView().findViewById(R.id.lviewFrame);

        ImageView sorchButton = (ImageView) getView().findViewById(R.id.sorchButton);

        sorchButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                ((MainActivity)getActivity()).openSerch();

            }
        });

        // адаптер
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(getContext(), android.R.layout.simple_spinner_item, data);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

        Spinner spinner = (Spinner) getView().findViewById(R.id.spinner);
        spinner.setAdapter(adapter);
        // заголовок
        spinner.setPrompt("Title");
        // выделяем элемент
//        spinner.setSelection(2);
        // устанавливаем обработчик нажатия
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                if(nowPosition ==position)
                    return;
                nowPosition=position;
                if(nowPosition ==0)
                {
                    ((MainActivity) getActivity()).order = "ORDER BY date_added";
                }
                if(nowPosition ==1)
                {
                    ((MainActivity) getActivity()).order = "ORDER BY cost";
                }
                if(nowPosition ==2)
                {
                    ((MainActivity) getActivity()).order = "ORDER BY cost desc";
                }

                ((MainActivity) getActivity()).SetMinFragment();
                Toast.makeText(getContext(), "Position = " + position, Toast.LENGTH_SHORT).show();
            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });

    }
    CustomListView customListView;
    Bitmap emptiImage;

    int size;

    View futer_view;
    ExampleAsyncTask exampleAsyncTask;
    @RequiresApi(api = Build.VERSION_CODES.M)
    public void collectData(Bitmap emptiImage, String result) {


        this.emptiImage = emptiImage;
        JSONArray ja = null;
        JSONObject jo;
        boolean error;
        try {
            ja = new JSONArray(result);
            jo = null;
            price = new ArrayList<String>();
            shortDisrip = new ArrayList<String>();
            imagepath = new ArrayList<String>();
            idDeliver = new ArrayList<String>();
            bitmaps = new ArrayList<Bitmap>();
            idHels = new ArrayList<String>();

        }   catch (JSONException e) {
            Log.e("Image:!!!","EROR 33");
            Log.e("Image:!!!",e.getMessage());
            //not_find_error.
            LinearLayout linearLayout = (LinearLayout) getView().findViewById(R.id.not_find_error);
            linearLayout.setVisibility(View.VISIBLE);
            listView.setAdapter(null);
            return;
        }

        for (int i = 0; i <= ja.length(); i++) {

            try {
                jo = ja.getJSONObject(i);
                size = jo.getInt("size");
                //if(size ==0)
                price.add(jo.getString("cost")) ;
                shortDisrip.add("г. "+jo.getString("human_settlements")+", ул. "+jo.getString("street"));//
                imagepath.add(getPhoto(jo.getString("photo"),1));
                idDeliver.add(jo.getString("id_realtys"));
                idHels.add(jo.getString("hels"));

                Log.e("SIZE", size+"");

                URL url = new URL(imagepath.get(i));
                Bitmap bmp = BitmapFactory.decodeStream(url.openConnection().getInputStream());
                bitmaps.add(bmp);
            } catch (JSONException e) {
                Log.e("TEST1","1111");
            } catch (MalformedURLException e) {
                Log.e("TEST1","2222");
            } catch (IOException e) {
                bitmaps.add(emptiImage);
                Log.e("TEST1","3333");
            }

        }

        customListView=new CustomListView(getActivity(),price,shortDisrip,imagepath,bitmaps);//        customListView.notifyDataSetChanged();
        //customListView.add();
        listView.setAdapter(customListView);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener()
        {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                customListView.notifyDataSetChanged();

                if(!new ConectionDetector(getContext()).isConected())
                {
                    Intent questionIntent = new Intent(getContext(), errorActivity.class);
                    startActivity(questionIntent);
                    return;
                }
                Intent questionIntent = new Intent(getActivity(), AboutActivity.class);
                questionIntent.putExtra("ID",idDeliver.get(position));
                questionIntent.putExtra("POS",position);
                startActivity(questionIntent);
                //overridePendingTransition(R.anim.top,R.anim.alpha);
                String selected = ((TextView) view.findViewById(R.id.tvemail)).getText().toString();

                //Reload();

            }
        });

        listView.setOnScrollChangeListener(new View.OnScrollChangeListener() {
            @Override
            public void onScrollChange(View v, int scrollX, int scrollY, int oldScrollX, int oldScrollY) {

                if(listView.getFirstVisiblePosition()>=listView.getCount()-5) {

                    if(!isAdded&&listView.getCount()<size)
                    {
                        isAdded = true;
                        //Toast.makeText(getContext(), ""+listView.getFirstVisiblePosition(), Toast.LENGTH_SHORT).show();
                        StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
                        exampleAsyncTask = new ExampleAsyncTask();
                        exampleAsyncTask.execute("SELECT a.cost,b.region AS region ,human_settlements,street,photo,id_realtys FROM realtys a, regions b WHERE a.id_region = b.id_region and a.state = '2' "+((MainActivity)getActivity()).GetSort()+" "+((MainActivity) getActivity()).order+" LIMIT "+LimitMin+","+LimitMax,"http://bomj.malaha.beget.tech/Home/Load.php");
                    }
                }
//                if (listView.getLastVisiblePosition() == listView.getAdapter().getCount() -1 &&
//                        listView.getChildAt(listView.getChildCount() - 1).getBottom() <= listView.getHeight())
//                {
//
//
//                    Toast.makeText(getContext(),"Last",Toast.LENGTH_LONG).show();
//
//                }
            }
        });
        ((GlopalParams) getActivity().getApplication()).setHels(idHels);

        //AddItem();


    }

    int LimitMax=2;
    int LimitMin= 6;
    public boolean isAdded =false;




    public void Reload()
    {
        customListView.notifyDataSetChanged();
    }

    //Основние процесы жизненного цикла ПО Изобразить схему расписать план
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
    ListView list;

    private class ExampleAsyncTask extends AsyncTask<String, String, String> {

        public ExampleAsyncTask()
        {

        }

        @Override
        protected void onPreExecute() {
            super.onPreExecute();

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

            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            result = Zaprps.result;
            try {
                AddItem(result);
            }catch (Exception e)
            {

            }
            return "Finished!";
        }
        int size = 0;
        private void AddItem(String result)
        {

            JSONArray ja = null;
            JSONObject jo;
            try {
                ja = new JSONArray(result);
                jo = null;


            }   catch (JSONException e) {
                Log.e("Image:!!!","EROR 33");
                Log.e("Image:!!!",e.getMessage());
            }
            for (int i = 0; i <= ja.length(); i++) {

                try {
                    jo = ja.getJSONObject(i);
                    price.add(jo.getString("cost")) ;
                    shortDisrip.add("г. "+jo.getString("human_settlements")+", ул. "+jo.getString("street"));//
                    imagepath.add(getPhoto(jo.getString("photo"),1));
                    idDeliver.add(jo.getString("id_realtys"));

                    URL url = new URL(imagepath.get(imagepath.size()-1));
                    Bitmap bmp = BitmapFactory.decodeStream(url.openConnection().getInputStream());
                    bitmaps.add(bmp);
                } catch (JSONException e) {
                    Log.e("TEST1","1111");
                } catch (MalformedURLException e) {
                    Log.e("TEST1","2222");
                } catch (IOException e) {
                    bitmaps.add(emptiImage);
                    Log.e("TEST1","3333");
                }

            }

            LimitMin+=2;
            isAdded = false;
        }

        @RequiresApi(api = Build.VERSION_CODES.M)
        @Override
        protected void onPostExecute(String s) {
            super.onPostExecute(s);

            try
            {
                Reload();
            }catch (Exception e)
            {

            }

            Log.e("RESULT: ",result);


        }
    }

}
