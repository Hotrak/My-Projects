package mbab.iso.com.materialbottomappbar;


import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.Switch;
import android.widget.Toast;


import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import com.google.android.material.bottomappbar.BottomAppBar;
import com.google.android.material.floatingactionbutton.FloatingActionButton;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;


public class MainActivity extends AppCompatActivity {

    private BottomAppBar bar;
    private Switch aSwitch;
    private boolean fbModeCenter = true;
    public FloatingActionButton fab;
    private FrameLayout frameLayout;

    private mainFragment mainFragment;
    private accayntFragment accayntFragment;
    private BlankFragment blankFragment;

    RelativeLayout progressBar;

    ListView listView;
    BufferedInputStream is;
    String line = null;
    String result = null;
    String resultAddData = null;
    String serchAddData = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        init();


//        saveText();

        int kod = openText();
        if(kod!=-1)
        {
            zapros Zaprps = new zapros("http://bomj.malaha.beget.tech/Home/CheckPass.php");

            Zaprps.start("SELECT `password`,`id` FROM users WHERE `email` = '"+login+"' AND `id` = '"+ kod+"'");
            try {
                Zaprps.join();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            int error =-1;
            JSONObject jo = null;
            try {
                jo = new JSONObject(Zaprps.result());
                error = jo.getInt("error");
                Log.e("SecsesMes",jo.getString("mess"));
            } catch (JSONException e) {
                e.printStackTrace();
            }

// get
            //String s = ((GlopalParams) this.getApplication()).getSomeVariable();

            if(error ==1)
            {

                Log.e("CHECK_ERRORS ",""+1);
                try {
                    ((GlopalParams) this.getApplication()).setId(jo.getString("id"));
                    ((GlopalParams) this.getApplication()).setLogin(login);
                    //Toast.makeText(this,jo.getString("id")+" p",Toast.LENGTH_LONG).show();
//                    Toast.makeText(this,"OPEN", Toast.LENGTH_SHORT).show();

                } catch (JSONException e) {
                    Log.e("CHECK_ERRORS ",""+2);
                }

            }
            else
                Log.e("ErrorInsert",error+"");

        }else
            Log.e("ErrorInsert","OJIBKA");

        fab.hide();
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
//                    if (fbModeCenter) {
//                    bar.setFabAlignmentMode(BottomAppBar.FAB_ALIGNMENT_MODE_END);
//                    fbModeCenter = false;
//                } else {
//                    bar.setFabAlignmentMode(BottomAppBar.FAB_ALIGNMENT_MODE_CENTER);
//                    fbModeCenter = true;
//                }

                if(((GlopalParams) getApplication()).getLogin() == "-1")//Integer.parseInt(((GlopalParams) this.getApplication()).getLogin())  == -1
                {
                    Intent questionIntent = new Intent(MainActivity.this, signIN.class);
                    startActivity(questionIntent);
                    return;
                }

                Intent questionIntent = new Intent(MainActivity.this, addActivity.class);
                questionIntent.putExtra("INFO",resultAddData);
                startActivity(questionIntent);
            }
        });


        bar.setNavigationOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                setFragmen(mainFragment);

            }
        });

        frameLayout = (FrameLayout) findViewById(R.id.frame);

        mainFragment = new mainFragment();
        accayntFragment = new accayntFragment();
        blankFragment = new BlankFragment();

        progressBar = findViewById(R.id.progressBar);
        setFragmen(mainFragment);

        addInfoLoad();
        serchInfoLoad();
    }


    public String order = "ORDER BY date_added";

    public void setBlanckFragment()
    {
        setFragmen(blankFragment);
    }
    public String GetResultAddData()
    {
        return resultAddData;
    }
    public String GetQuery(){return "SELECT (SELECT COUNT(1) FROM realtys) as size,a.cost,b.region AS region ,human_settlements,street,photo,id_realtys,(SELECT COUNT(desires.id) FROM desires WHERE desires.id_realtys = a.id_realtys and  desires.id = "+ ((GlopalParams) this.getApplication()).getId() +"  ) AS hels FROM realtys a, regions b WHERE a.id_region = b.id_region and a.state = '2' "+sort+" "+order+" LIMIT 6";}


    public String GetSort()
    {
        return sort;
    }

    private void addInfoLoad()
    {
        ExampleAsyncTask asyncTask = new ExampleAsyncTask(1);
        asyncTask.execute("1","http://bomj.malaha.beget.tech/Home/addInfoLoader.php");
    }
    private void serchInfoLoad()
    {
        ExampleAsyncTask asyncTask = new ExampleAsyncTask(2);
        asyncTask.execute("1","http://bomj.malaha.beget.tech/Home/serchInfoLoad.php");
    }

    public boolean addPhoto(MenuItem item) {
        //mostrarMensaje("ADD PHOTO");
//        Intent questionIntent = new Intent(MainActivity.this, MyWantActivity.class);
//        //questionIntent.putExtra("EXTRA_MESSAGE",);
//        startActivity(questionIntent);

        setFragmen(blankFragment);
        return true;
    }

    public boolean accaunt(MenuItem item) {

        setFragmen(accayntFragment);



        //mostrarMensaje("SEARCH "+" "+((GlopalParams) this.getApplication()).getId());
        return true;
    }

    private void mostrarMensaje(String mensaje) {
        Toast.makeText(getBaseContext(), mensaje, Toast.LENGTH_SHORT).show();
    }

    private void init() {
        this.bar = findViewById(R.id.bar);
        setSupportActionBar(bar);
        this.aSwitch = findViewById(R.id.switch1);
        this.fab = findViewById(R.id.fab);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater menuInflater = getMenuInflater();
        menuInflater.inflate(R.menu.menu_bbar, menu);
        return true;
    }


    boolean isOpen;
    Fragment setFragment;
    public void setFragmen(Fragment fragmen)
    {
        if(!new ConectionDetector(this).isConected())
        {
            Intent questionIntent = new Intent(MainActivity.this, errorActivity.class);
            startActivity(questionIntent);
            return;
        }
        if(setFragment!= fragmen)
        {



            if((fragmen == accayntFragment || fragmen == blankFragment) &&((GlopalParams) this.getApplication()).getLogin() == "-1")//Integer.parseInt(((GlopalParams) this.getApplication()).getLogin())  == -1
            {
                Intent questionIntent = new Intent(MainActivity.this, signIN.class);
                startActivity(questionIntent);
                return;
            }
            FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
            fragmentTransaction.replace(R.id.frame,fragmen);
            fragmentTransaction.commit();
            setFragment =fragmen;

            if(setFragment ==mainFragment)
            {
                SetMinFragment();

            }
            if(setFragment ==accayntFragment)
            {
                StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
                ExampleAsyncTask exampleAsyncTask = new ExampleAsyncTask();
                exampleAsyncTask.execute("SELECT (SELECT COUNT(realtys.id) FROM realtys WHERE realtys.id = users.id) AS cop,name,phone,coin,(SELECT COUNT(desires.id) FROM desires WHERE desires.id = users.id) as want FROM users WHERE users.id ="+ ((GlopalParams) this.getApplication()).getId(),"http://bomj.malaha.beget.tech/Home/Load.php");
            }
            if(setFragment == blankFragment)
            {
                StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
                ExampleAsyncTask exampleAsyncTask = new ExampleAsyncTask();
                exampleAsyncTask.execute("SELECT b.region AS region ,human_settlements,street,photo,a.id_realtys,c.id_desire FROM realtys a, regions b,desires c WHERE a.id_region = b.id_region and c.id_realtys = a.id_realtys and c.id = "+((GlopalParams) this.getApplication()).getId(),"http://bomj.malaha.beget.tech/Home/Load.php");

            }

            //((GlopalParams) this.getApplication()).getId()

        }
    }
    public void SetMinFragment()
    {
        StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));
        ExampleAsyncTask exampleAsyncTask = new ExampleAsyncTask();
        exampleAsyncTask.execute(GetQuery(),"http://bomj.malaha.beget.tech/Home/Load.php");
    }
    public void openSerch()
    {
        Intent questionIntent = new Intent(this, serchActivity.class);
        questionIntent.putExtra("DATA",(serchAddData));
        startActivityForResult(questionIntent,0);
    }

    private String sort ="";
    @RequiresApi(api = Build.VERSION_CODES.N)
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {

        super.onActivityResult(requestCode, resultCode, data);
        if (resultCode == this.RESULT_CANCELED) {
            return;
        }

        Log.e("RESULT_SERCH","ppp:"+requestCode);

        if (requestCode == 0) {
            if (resultCode == RESULT_OK) {


                sort = data.getStringExtra("RESULT");
                Log.e("ZAPROS",GetQuery());
                ExampleAsyncTask exampleAsyncTask = new ExampleAsyncTask();
                exampleAsyncTask.execute(GetQuery(),"http://bomj.malaha.beget.tech/Home/Load.php");

                Log.e("RESULT_SERCH",sort);

            }else {
            }
        }

    }

    public void Message(String message) {

        Toast.makeText(this, message, Toast.LENGTH_LONG).show();
    }

    public void saveText(){

        FileOutputStream fos = null;
        try {

            String text = "123";
            fos = openFileOutput("content.txt", MODE_PRIVATE);
            fos.write(text.getBytes());
            //Toast.makeText(this, "Файл сохранен", Toast.LENGTH_SHORT).show();
        }
        catch(IOException ex) {

            Toast.makeText(this, ex.getMessage(), Toast.LENGTH_SHORT).show();
        }
        finally{
            try{
                if(fos!=null)
                    fos.close();
            }
            catch(IOException ex){

                Toast.makeText(this, ex.getMessage(), Toast.LENGTH_SHORT).show();
            }
        }
    }
    String login;
    public int openText(){

        FileInputStream fin = null;

        try {
            fin = openFileInput("content.txt");
            byte[] bytes = new byte[fin.available()];
            fin.read(bytes);
            String text = new String (bytes);

            String[] strs = text.split("-");
            login = strs[1];
            //Toast.makeText(this, text , Toast.LENGTH_SHORT).show();

            return Integer.parseInt(strs[0]);
            //return -1;

        }
        catch(IOException ex) {

            return -1;
        }
        finally{

            try{
                if(fin!=null)
                    fin.close();
            }
            catch(IOException ex){

            }
        }
    }

    private String findFile(String name)
    {

       return getFileStreamPath("name").getPath();
    }


    Bitmap emptiImage;
    private class ExampleAsyncTask extends AsyncTask<String, String, String> {


        public ExampleAsyncTask()
        {

        }
        boolean onliQwier = false;
        int mes = -1;

        public ExampleAsyncTask(int mes)
        {
            this.mes = mes;
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

            if(mes ==-1)
                result = Zaprps.result;
            if(mes ==1)
                resultAddData = Zaprps.result;
            if(mes ==2)
                serchAddData = Zaprps.result;

            return "Finished!";
        }

        @RequiresApi(api = Build.VERSION_CODES.M)
        @Override
        protected void onPostExecute(String s) {
            super.onPostExecute(s);

            progressBar.setVisibility(View.INVISIBLE);

            if(mes==-1)
            {
                Log.e("RESULT: ",result.trim());
                if(setFragment == mainFragment)
                    mainFragment.collectData(emptiImage,result);
                if(setFragment == accayntFragment)
                    accayntFragment.LoadData(result);
                if(setFragment == blankFragment)
                {
                    blankFragment.collectData(emptiImage,result);

                }




            }else
                fab.show();

        }
    }

}




