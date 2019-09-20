package mbab.iso.com.materialbottomappbar;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.accounts.Account;
import android.accounts.AccountAuthenticatorActivity;
import android.accounts.AccountManager;
import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Rect;
import android.icu.util.Calendar;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Base64;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewTreeObserver;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.sql.Date;
import java.text.SimpleDateFormat;
import java.util.ArrayList;

public class addActivity extends AppCompatActivity {

    private LinearLayout photoLoadingButton;


    private LinearLayout typeDialButton;
    private LinearLayout areaButton;
    private LinearLayout regeonButton;
    private LinearLayout typeRealatyButton;
    private LinearLayout typeHoseButton;
    private LinearLayout typeRemButton;
    private LinearLayout typePlanButton;

    RelativeLayout contentView;

    private ArrayList<Bitmap> bitmaps;
    private String[] imgsName;
    ImageView[] imageViews;
    public RelativeLayout progressBar;
    public RelativeLayout bottomButton;
    public boolean isOpenKeyBord;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add);



        bitmaps = new ArrayList<Bitmap>();
        imgsName = new String[9];
        imageViews = new ImageView[9];

        imageViews[0] = findViewById(R.id.photo1);
        imageViews[1] = findViewById(R.id.photo2);
        imageViews[2] = findViewById(R.id.photo3);
        imageViews[3] = findViewById(R.id.photo4);
        imageViews[4] = findViewById(R.id.photo5);
        imageViews[5] = findViewById(R.id.photo6);
        imageViews[6] = findViewById(R.id.photo7);
        imageViews[7] = findViewById(R.id.photo8);
        imageViews[8] = findViewById(R.id.photo9);

        typeDialButton = findViewById(R.id.typeDialButton);
        areaButton = findViewById(R.id.areaButton);
        regeonButton = findViewById(R.id.regeonButton);
        typeRealatyButton = findViewById(R.id.typeRealatyButton);
        typeHoseButton = findViewById(R.id.typeHoseButton);
        typeRemButton = findViewById(R.id.typeRemButton);
        typePlanButton = findViewById(R.id.typePlanButton);

        regeonButton.setVisibility(View.GONE);

        //typeDialButton.setVisibility(View.GONE);
        progressBar = findViewById(R.id.progressBar);


        String data = getIntent().getSerializableExtra("INFO").toString();

        ExecuuteData(data);

        View.OnClickListener onClickListener = new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                ViewGroup vg = (ViewGroup) v;
                idViewButton = v.getId();
                idView = vg.getChildAt(1).getId();
                if(v.getId() == R.id.typeDialButton) { OpenList(durationsData,"duration"); }
                if(v.getId() == R.id.areaButton) { OpenList(areasData,"area"); }
                if(v.getId() == R.id.regeonButton) { OpenList(regeonData,"region",areasID); }
                if(v.getId() == R.id.typeRealatyButton) { OpenList(typeRealatyData,"type_realty"); }
                if(v.getId() == R.id.typeHoseButton) { OpenList(typeHome_Data,"type_house"); }
                if(v.getId() == R.id.typeRemButton) { OpenList(typeRemData,"type_upkeep"); }
                if(v.getId() == R.id.typePlanButton) { OpenList(typePlanData,"type_layout"); }
                //if(v.getId() == R.id.rumCountButton) { OpenList(new String[]{"1","2","3","4","5","6","7","8","9","10","11","12"}); }
            }
        };

        typeDialButton.setOnClickListener(onClickListener);
        areaButton.setOnClickListener(onClickListener);
        regeonButton.setOnClickListener(onClickListener);
        typeRealatyButton.setOnClickListener(onClickListener);
        typeHoseButton.setOnClickListener(onClickListener);
        typeRemButton.setOnClickListener(onClickListener);
        typePlanButton.setOnClickListener(onClickListener);

        //typeDialButton.getView


        photoLoadingButton = findViewById(R.id.loadImageButton);
        photoLoadingButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent galleryIntent = new Intent(Intent.ACTION_PICK,
                        android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
                startActivityForResult(galleryIntent, GALLERY);
            }
        });

        bottomButton = findViewById(R.id.bottomButton);

        contentView = findViewById(R.id.addRoot);
        contentView.getViewTreeObserver().addOnGlobalLayoutListener(
                new ViewTreeObserver.OnGlobalLayoutListener() {
                    @Override
                    public void onGlobalLayout() {

                        Rect r = new Rect();
                        contentView.getWindowVisibleDisplayFrame(r);
                        int screenHeight = contentView.getRootView().getHeight();

                        // r.bottom is the position above soft keypad or device button.
                        // if keypad is shown, the r.bottom is smaller than that before.
                        int keypadHeight = screenHeight - r.bottom;

                       // Log.d(TAG, "keypadHeight = " + keypadHeight);

                        if (keypadHeight > screenHeight * 0.15) { // 0.15 ratio is perhaps enough to determine keypad height.

                            isOpenKeyBord = true;
                            bottomButton.setGravity(Gravity.RIGHT);
                            ViewGroup vg = (ViewGroup) bottomButton;
                            TextView textView = (TextView) vg.getChildAt(0);
                            textView.setText("Готово");

                        }
                        else {

                            isOpenKeyBord = false;
                            bottomButton.setGravity(Gravity.CENTER);
                            ViewGroup vg = (ViewGroup) bottomButton;
                            TextView textView = (TextView) vg.getChildAt(0);
                            textView.setText("Подать объявление");
                        }
                    }
                });


        bottomButton.setOnClickListener(new View.OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.N)
            @Override
            public void onClick(View v) {
                if(isOpenKeyBord)
                    hideKeyboard(addActivity.this);
                else {
                    Insert();
                    //Toast.makeText(addActivity.this, TestJecon(), Toast.LENGTH_LONG).show();
                }

                    //Insert();


            }
        });
    }
    String areasData;
    String durationsData;
    String regeonData;
    String typeRealatyData;
    String typeHome_Data;
    String typeRemData;
    String typePlanData;
    public static void hideKeyboard(Activity activity) {
        InputMethodManager imm = (InputMethodManager) activity.getSystemService(Activity.INPUT_METHOD_SERVICE);
        //Find the currently focused view, so we can grab the correct window token from it.
        View view = activity.getCurrentFocus();
        //If no view currently has focus, create a new one, just so we can grab a window token from it
        if (view == null) {
            view = new View(activity);
        }
        imm.hideSoftInputFromWindow(view.getWindowToken(), 0);
    }
    private void OpenList(String mess,String tag)
    {
        Intent questionIntent = new Intent(addActivity.this, listActivity.class);
        questionIntent.putExtra("LIST",mess);
        questionIntent.putExtra("TAG",tag);
        startActivityForResult(questionIntent,0);
        overridePendingTransition(R.anim.right,R.anim.alpha);
    }
    private void OpenList(String mess,String tag,String id)
    {
        Intent questionIntent = new Intent(addActivity.this, listActivity.class);
        questionIntent.putExtra("LIST",mess);
        questionIntent.putExtra("TAG",tag);
        questionIntent.putExtra("ID",id);
        startActivityForResult(questionIntent,0);


    }

    private void ExecuuteData(String data)
    {
        JSONObject jo = null;
        JSONArray ja = null;

        try {
            //ja = new JSONArray(data);
            jo = new JSONObject(data);
        } catch (JSONException e) {
            Log.e("EXECUTEerror","Tot");

            e.printStackTrace();
        }

        try {
            durationsData = jo.getString("durations");
            areasData = jo.getString("areas");
            regeonData = jo.getString("regions");
            typeRealatyData = jo.getString("type_realtys");
            typeHome_Data = jo.getString("type_houses");
            typeRemData = jo.getString("type_upkeeps");
            typePlanData = jo.getString("type_layouts");
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    int idView;
    int idViewButton;


    int count = 0;
    int countImages = 0;
    @RequiresApi(api = Build.VERSION_CODES.N)
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {

        super.onActivityResult(requestCode, resultCode, data);
        if (resultCode == this.RESULT_CANCELED) {
            return;
        }
        if (requestCode == GALLERY) {
            if (data != null) {
                Uri contentURI = data.getData();
                try {

                    Bitmap bitmap = MediaStore.Images.Media.getBitmap(this.getContentResolver(), contentURI);
                    imgsName[countImages]=(String.valueOf(Calendar.getInstance().getTimeInMillis())+".jpg");
                    countImages++;
                    bitmaps.add(bitmap);
                    imageViews[count].setImageBitmap(bitmaps.get(count));
                    count++;
                    //imageView.setImageBitmap(bitmap);

                } catch (IOException e) {
                    e.printStackTrace();
                    Toast.makeText(this, "Failed!", Toast.LENGTH_SHORT).show();
                }
            }
        }


        if (requestCode == 0) {
            if (resultCode == RESULT_OK) {
                String thiefname = data.getStringExtra("RESULT");
                TextView infoTextView = (TextView) findViewById(idView);
                infoTextView.setText(thiefname);

                String id = data.getStringExtra("RESULT_ID");

                if(idViewButton == R.id.typeDialButton) { durationsID =id; }
                if(idViewButton == R.id.areaButton) { areasID =id;  regeonButton.setVisibility(View.VISIBLE);}
                if(idViewButton == R.id.regeonButton) { regeonID =id; }
                if(idViewButton == R.id.typeRealatyButton) { typeRealatyID =id;  }
                if(idViewButton == R.id.typeHoseButton) { typeHome_ID =id;  }
                if(idViewButton == R.id.typeRemButton) { typeRemID =id;  }
                if(idViewButton == R.id.typePlanButton) { typePlanID =id;  }

            }else {
            }
        }

    }

    String areasID="-1";
    String durationsID="-1";
    String regeonID="-1";
    String typeRealatyID ="0";
    String typeHome_ID ="0";
    String typeRemID ="0";
    String typePlanID ="0";

    private final int GALLERY = 1;
    JSONObject jsonObject;
    RequestQueue rQueue;
    private String upload_URL = "http://bomj.malaha.beget.tech/Home/uploadVolley.php";

    @RequiresApi(api = Build.VERSION_CODES.N)
    private void uploadImage(Bitmap bitmap, String name){

        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.JPEG, 100, byteArrayOutputStream);
        String encodedImage = Base64.encodeToString(byteArrayOutputStream.toByteArray(), Base64.DEFAULT);
        try {
            jsonObject = new JSONObject();
            //String imgname = String.valueOf(Calendar.getInstance().getTimeInMillis());
//            String imgname = "123";
            jsonObject.put("name", name);
            //  Log.e("Image name", etxtUpload.getText().toString().trim());
            jsonObject.put("image", encodedImage);
            // jsonObject.put("aa", "aa");
        } catch (JSONException e) {
            Log.e("JSONObject Here", e.toString());
        }
        JsonObjectRequest jsonObjectRequest = new JsonObjectRequest(Request.Method.POST, upload_URL, jsonObject,
                new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject jsonObject) {
                        Log.e("1", jsonObject.toString());
                        rQueue.getCache().clear();
                        Toast.makeText(getApplication(), "Image Uploaded Successfully", Toast.LENGTH_SHORT).show();
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError volleyError) {
                Log.e("2", volleyError.toString());

            }
        });

        rQueue = Volley.newRequestQueue(addActivity.this);
        rQueue.add(jsonObjectRequest);

    }
    @RequiresApi(api = Build.VERSION_CODES.N)
    private void Insert()
    {

        if(Integer.parseInt(durationsID) ==-1)
        {
            Toast.makeText(this,"Вид сделки не выбран",Toast.LENGTH_LONG).show();
            return;
        }
        if(Integer.parseInt(areasID) ==-1)
        {
            Toast.makeText(this,"Область не выбрана",Toast.LENGTH_LONG).show();;
            return;
        }

        if(Integer.parseInt(regeonID) ==-1)
        {
            Toast.makeText(this,"Регион не выбран",Toast.LENGTH_LONG).show();;
            return;
        }
        EditText townEdit = findViewById(R.id.town);
        EditText ylicaEdit = findViewById(R.id.ylica);
        EditText rumCountEdit = findViewById(R.id.rumCount);
        EditText atajCountEdit = findViewById(R.id.atajCount);
        EditText atajEdit = findViewById(R.id.ataj);

        EditText ouerAreaEdit = findViewById(R.id.ouerArea);
        EditText liveAreaEdit = findViewById(R.id.liveArea);
        EditText kykAreaEdit = findViewById(R.id.kykArea);

        EditText yerEdit = findViewById(R.id.yer);
        EditText priceEdit = findViewById(R.id.price);
        EditText dicriprionEdit = findViewById(R.id.dicriprion);

        String photo = TestJecon();

        CheckBox mebCheck = findViewById(R.id.mebCheck);
        CheckBox kykMebCheck = findViewById(R.id.kykMebCheck);
        CheckBox frizCheck = findViewById(R.id.frizCheck);
        CheckBox balconCheck = findViewById(R.id.balconCheck);
        CheckBox inetCheck = findViewById(R.id.inetCheck);
        CheckBox kondicCheck = findViewById(R.id.kondicCheck);
        CheckBox woshMashinCheck = findViewById(R.id.woshMashinCheck);

        int mebVel = mebCheck.isChecked()  ? 1:0;
        int kykMebVel = kykMebCheck.isChecked()  ? 1:0;
        int frizVel = frizCheck.isChecked()  ? 1:0;
        int balconVel = balconCheck.isChecked()  ? 1:0;
        int inetVel = inetCheck.isChecked()  ? 1:0;
        int kondicVel = kondicCheck.isChecked()  ? 1:0;
        int woshMashinVel = woshMashinCheck.isChecked()  ? 1:0;


        for(int i =0; i<bitmaps.size();i++)
            uploadImage(bitmaps.get(i), imgsName[i]);

        java.util.Date dateNow = new java.util.Date();
        SimpleDateFormat formatForDateNow = new SimpleDateFormat("yyyy.MM.dd hh:mm:ss");

        //System.out.println("Текущая дата " + formatForDateNow.format(dateNow));



        String query = "INSERT INTO `realtys` ( `id`, `id_duration`, `id_type_realty`, `id_type_house`, `id_type_upkeep`, `id_type_layout`, `id_area`, `id_region`, `human_settlements`, `street`, `house_number`, `full_area`, `living_area`, `kitchen_area`, `floor`, `count_floor`, `room`, `photo`, `description`, `cost`, `state`, `furniture`, `kitchen_furniture`, `refrigerator`, `washing_machine`, `Internet`, `loggia_or_balcony`, `air_conditioning`, `date_added`, `date_change`, `date_build`, `location_x`, `location_y`) VALUES" +
                                            " ("+((GlopalParams) this.getApplication()).getId()+", '"+durationsID+"', '"+typeRealatyID+"', '"+typeHome_ID+"', '"+typeRemID+"', '"+typePlanID+"', '"+areasID+"', '"+regeonID+"', '"+townEdit.getText()+"', '"+ylicaEdit.getText()+"', '"+areasID+"', '"+ouerAreaEdit.getText()+"', '"+liveAreaEdit.getText()+"', '"+kykAreaEdit.getText()+"', '"+atajEdit.getText()+"', '"+atajCountEdit.getText()+"', '"+rumCountEdit.getText()+"', '"+photo+"', '"+dicriprionEdit.getText()+"','"+priceEdit.getText()+"', 2, '"+mebVel+"', '"+kykMebVel+"', '"+frizVel+"', '"+woshMashinVel+"', '"+inetVel+"', '"+balconVel+"', '"+kondicVel+"', '"+formatForDateNow.format(dateNow)+"', '"+formatForDateNow.format(dateNow)+"', '"+yerEdit.getText()+"', '0', '0')";
        ExampleAsyncTask task = new ExampleAsyncTask();
        task.execute(query,"http://bomj.malaha.beget.tech/Home/Insert.php");
    }
    public String TestJecon()
    {

        JSONArray arr = new JSONArray();
        JSONObject obj = new JSONObject();





        return "{\"1\": {\"photo\" : \""+imgsName[0]+"\"}, \"2\": {\"photo\" : \""+imgsName[1]+"\"}, \"3\":{\"photo\" : \""+imgsName[2]+"" +
                " \"}, \"4\": {\"photo\" : \""+imgsName[3]+"\"}, \"5\": {\"photo\" : \""+imgsName[4]+"\"}, \"6\":{\"photo\" : \""+imgsName[5]+"" +
                " \"}, \"7\": {\"photo\" : \""+imgsName[6]+"\"}, \"8\":{\"photo\" : \""+imgsName[7]+"\"}, \"9\":{\"photo\" : \""+imgsName[8]+"\"}}";
    }
    private boolean checkErrors(String result)
    {
        int error =-1;
        JSONObject jo = null;
        try {
            jo = new JSONObject(result);
            error = jo.getInt("error");
        } catch (JSONException e) {
            e.printStackTrace();
        }


        if(error ==1)
            Log.e("SecsesInsert",error+"");
        else
            Log.e("ErrorInsert",error+"");


        return  error ==1;
    }
    String result = null;


    public void Close()
    {
        finish();
    }
    private class ExampleAsyncTask extends AsyncTask<String, String, String> {


        @Override
        protected void onPreExecute() {
            super.onPreExecute();

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


            progressBar.setVisibility(View.INVISIBLE);

            Close();

        }
    }
}
