package mbab.iso.com.materialbottomappbar;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Rect;
import android.icu.util.Calendar;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewTreeObserver;
import android.view.inputmethod.InputMethodManager;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.ArrayList;

public class serchActivity extends AppCompatActivity {

    private LinearLayout typeDialButton;
    private LinearLayout areaButton;
    private LinearLayout regeonButton;
    private LinearLayout typeRealatyButton;
    private LinearLayout typeHoseButton;
    private LinearLayout typeRemButton;
    private LinearLayout typePlanButton;

    RelativeLayout contentView;

    public RelativeLayout bottomButton;
    public boolean isOpenKeyBord;

    private EditText rumCountEdit1;
    private EditText rumCountEdit2;

    private EditText atajCountEdit1;
    private EditText atajCountEdit2;

    private EditText atajEdit1;
    private EditText atajEdit2;

    private EditText ouerAreaEdit1;
    private EditText ouerAreaEdit2;

    private EditText liveAreaEdit1;
    private EditText liveAreaEdit2;

    private EditText kykAreaEdit1;
    private EditText kykAreaEdit2;

    private EditText priceEdit1;
    private EditText priceEdit2;

    CheckBox mebCheck ;
    CheckBox kykMebCheck;
    CheckBox frizCheck;
    CheckBox balconCheck;
    CheckBox inetCheck;
    CheckBox kondicCheck;
    CheckBox woshMashinCheck;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sorch);
//
        rumCountEdit1 = findViewById(R.id.rumCount1);
        rumCountEdit2 = findViewById(R.id.rumCount2);

        atajCountEdit1 = findViewById(R.id.atajCount1);
        atajCountEdit2 = findViewById(R.id.atajCount2);

        atajEdit1 = findViewById(R.id.ataj1);
        atajEdit2 = findViewById(R.id.ataj2);

        ouerAreaEdit1 = findViewById(R.id.ouerArea1);
        ouerAreaEdit2 = findViewById(R.id.ouerArea2);

        liveAreaEdit1 = findViewById(R.id.liveArea1);
        liveAreaEdit2 = findViewById(R.id.liveArea2);

        kykAreaEdit1 = findViewById(R.id.kykArea1);
        kykAreaEdit2 = findViewById(R.id.kykArea2);

        priceEdit1 = findViewById(R.id.price1);
        priceEdit2 = findViewById(R.id.price2);
//
        rumCountEdit1.setText(((GlopalParams) this.getApplication()).getRumCountEdit1());
        rumCountEdit2.setText(((GlopalParams) this.getApplication()).getRumCountEdit2());

        atajCountEdit1.setText(((GlopalParams) this.getApplication()).getAtajCountEdit1());
        atajCountEdit2.setText(((GlopalParams) this.getApplication()).getAtajCountEdit2());

        atajEdit1.setText(((GlopalParams) this.getApplication()).getAtajEdit1());
        atajEdit2.setText(((GlopalParams) this.getApplication()).getAtajEdit2());

        ouerAreaEdit1.setText(((GlopalParams) this.getApplication()).getOuerAreaEdit1());
        ouerAreaEdit2.setText(((GlopalParams) this.getApplication()).getOuerAreaEdit2());

        liveAreaEdit1.setText(((GlopalParams) this.getApplication()).getLiveAreaEdit1());
        liveAreaEdit2.setText(((GlopalParams) this.getApplication()).getLiveAreaEdit2());

        kykAreaEdit1.setText(((GlopalParams) this.getApplication()).getKykAreaEdit1());
        kykAreaEdit2.setText(((GlopalParams) this.getApplication()).getKykAreaEdit2());

        priceEdit1.setText(((GlopalParams) this.getApplication()).getPriceEdit1());
        priceEdit2.setText(((GlopalParams) this.getApplication()).getPriceEdit2());

        mebCheck = findViewById(R.id.mebCheck);
        kykMebCheck = findViewById(R.id.kykMebCheck);
        frizCheck = findViewById(R.id.frizCheck);
        balconCheck = findViewById(R.id.balconCheck);
        inetCheck = findViewById(R.id.inetCheck);
        kondicCheck = findViewById(R.id.kondicCheck);
        woshMashinCheck = findViewById(R.id.woshMashinCheck);

        mebCheck.setChecked(((GlopalParams) this.getApplication()).getMebCheck()=="1");
        kykMebCheck.setChecked(((GlopalParams) this.getApplication()).getKykMebCheck()=="1");
        frizCheck.setChecked(((GlopalParams) this.getApplication()).getFrizCheck()=="1");
        balconCheck.setChecked(((GlopalParams) this.getApplication()).getBalconCheck()=="1");
        inetCheck.setChecked(((GlopalParams) this.getApplication()).getInetCheck()=="1");
        kondicCheck.setChecked(((GlopalParams) this.getApplication()).getKondicCheck()=="1");
        woshMashinCheck.setChecked(((GlopalParams) this.getApplication()).getWoshMashinCheck()=="1");


        typeDialButton = findViewById(R.id.typeDialButton);
        areaButton = findViewById(R.id.areaButton);
        regeonButton = findViewById(R.id.regeonButton);
        typeRealatyButton = findViewById(R.id.typeRealatyButton);
        typeHoseButton = findViewById(R.id.typeHoseButton);
        typeRemButton = findViewById(R.id.typeRemButton);
        typePlanButton = findViewById(R.id.typePlanButton);

        regeonButton.setVisibility(View.GONE);

        //typeDialButton.setVisibility(View.GONE);

        String data = getIntent().getSerializableExtra("DATA").toString();

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
                            textView.setText("Показать");
                        }
                    }
                });


        bottomButton.setOnClickListener(new View.OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.N)
            @Override
            public void onClick(View v) {
                if(isOpenKeyBord)
                    hideKeyboard(serchActivity.this);
                else {
                    Search();
                    //Toast.makeText(addActivity.this, TestJecon(), Toast.LENGTH_LONG).show();
                }

                //Insert();


            }
        });



    }

    @Override
    public void onSaveInstanceState(Bundle savedInstanceState) {
        super.onSaveInstanceState(savedInstanceState);

        savedInstanceState.putString("1", "100");
        savedInstanceState.putDouble("myDouble", 1.9);
        savedInstanceState.putInt("MyInt", 1);
        savedInstanceState.putString("MyString", "Welcome back to Android");
        // etc.
    }
    public void onRestoreInstanceState(Bundle savedInstanceState) {
        super.onRestoreInstanceState(savedInstanceState);
        if(savedInstanceState!= null)
        {
            String savePrice = savedInstanceState.getString("1");
            Log.e("PTACE_EQUL = ",savePrice);

        }
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
        Intent questionIntent = new Intent(serchActivity.this, listActivity.class);
        questionIntent.putExtra("LIST",mess);
        questionIntent.putExtra("TAG",tag);
        startActivityForResult(questionIntent,1);
    }
    private void OpenList(String mess,String tag,String id)
    {
        Intent questionIntent = new Intent(serchActivity.this, listActivity.class);
        questionIntent.putExtra("LIST",mess);
        questionIntent.putExtra("TAG",tag);
        questionIntent.putExtra("ID",id);
        startActivityForResult(questionIntent,1);
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

        if (requestCode == 1) {
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
    String typeRealatyID ="-1";
    String typeHome_ID ="-1";
    String typeRemID ="-1";
    String typePlanID ="-1";


    private String getValue(String val_1,String val_2,String tag)
    {
        if(val_1.equals("")&& val_2.equals(""))
            return "";
        if(val_1.equals("") && !val_2.equals(""))
            return " and "+tag+" < "+val_2;
        if(!val_1.equals("")&& val_2.equals(""))
            return " and "+tag+" > "+val_1;
        if(!val_1.equals("")&& !val_2.equals(""))
            return " and "+tag+" > "+val_2+" and "+tag+" < "+val_1;

        return "";
    }

    private String getValue(int val_,String tag)
    {
        if(val_==1)
            return " and "+tag+" = 1";
        return "";
    }

    private String getValueID(int val_,String tag)
    {
        if(val_!=-1)
            return " and " + tag + " = " + val_;
        return "";
    }

    Intent answerIntent;

    private String getSerch()
    {

//        EditText rumCountEdit1 = findViewById(R.id.rumCount1);
//        EditText rumCountEdit2 = findViewById(R.id.rumCount2);
//
//        EditText atajCountEdit1 = findViewById(R.id.atajCount1);
//        EditText atajCountEdit2 = findViewById(R.id.atajCount2);
//
//        EditText atajEdit1 = findViewById(R.id.ataj1);
//        EditText atajEdit2 = findViewById(R.id.ataj2);
//
//        EditText ouerAreaEdit1 = findViewById(R.id.ouerArea1);
//        EditText ouerAreaEdit2 = findViewById(R.id.ouerArea2);
//
//        EditText liveAreaEdit1 = findViewById(R.id.liveArea1);
//        EditText liveAreaEdit2 = findViewById(R.id.liveArea2);
//
//        EditText kykAreaEdit1 = findViewById(R.id.kykArea1);
//        EditText kykAreaEdit2 = findViewById(R.id.kykArea2);
//
//        EditText priceEdit1 = findViewById(R.id.price1);
//        EditText priceEdit2 = findViewById(R.id.price2);





        ((GlopalParams) this.getApplication()).setRumCountEdit1(rumCountEdit1.getText().toString());
        ((GlopalParams) this.getApplication()).setRumCountEdit2(rumCountEdit2.getText().toString());

        ((GlopalParams) this.getApplication()).setAtajCountEdit1(atajCountEdit1.getText().toString());
        ((GlopalParams) this.getApplication()).setAtajCountEdit2(atajCountEdit2.getText().toString());

        ((GlopalParams) this.getApplication()).setAtajEdit1(atajEdit1.getText().toString());
        ((GlopalParams) this.getApplication()).setAtajEdit2(atajEdit2.getText().toString());

        ((GlopalParams) this.getApplication()).setOuerAreaEdit1(ouerAreaEdit1.getText().toString());
        ((GlopalParams) this.getApplication()).setOuerAreaEdit2(ouerAreaEdit2.getText().toString());

        ((GlopalParams) this.getApplication()).setLiveAreaEdit1(liveAreaEdit1.getText().toString());
        ((GlopalParams) this.getApplication()).setLiveAreaEdit2(liveAreaEdit2.getText().toString());

        ((GlopalParams) this.getApplication()).setKykAreaEdit1(kykAreaEdit1.getText().toString());
        ((GlopalParams) this.getApplication()).setKykAreaEdit2(kykAreaEdit2.getText().toString());

        ((GlopalParams) this.getApplication()).setPriceEdit1(priceEdit1.getText().toString());
        ((GlopalParams) this.getApplication()).setPriceEdit2(priceEdit2.getText().toString());



        String rumCount = getValue(rumCountEdit1.getText().toString(),rumCountEdit2.getText().toString(),"room");

        String atajCount = getValue(atajCountEdit1.getText().toString(),atajCountEdit2.getText().toString(),"count_floor");

        String ataj = getValue(atajEdit1.getText().toString(),atajEdit2.getText().toString(),"floor");

        String ouerArea = getValue(ouerAreaEdit1.getText().toString(),ouerAreaEdit2.getText().toString(),"full_area");

        String liveArea = getValue(liveAreaEdit1.getText().toString(),liveAreaEdit2.getText().toString(),"living_area");

        String kykArea = getValue(kykAreaEdit1.getText().toString(),kykAreaEdit2.getText().toString(),"kitchen_area");

        String price= getValue(priceEdit1.getText().toString(),priceEdit2.getText().toString(),"cost");

        String mebVel = (String) getValue(mebCheck.isChecked() ? 1 : 0,"furniture");
        String kykMebVel = (String) getValue(kykMebCheck.isChecked()  ? 1:0,"kitchen_furniture");
        String frizVel = (String) getValue(frizCheck.isChecked()  ? 1:0,"refrigerator");
        String balconVel = (String) getValue(balconCheck.isChecked()  ? 1:0,"loggia_or_balcony");
        String inetVel = (String) getValue(inetCheck.isChecked()  ? 1:0,"Internet");
        String kondicVel = (String) getValue(kondicCheck.isChecked()  ? 1:0,"air_conditioning");
        String woshMashinVel = (String) getValue(woshMashinCheck.isChecked()  ? 1:0,"washing_machine");

        ((GlopalParams) this.getApplication()).setMebCheck(mebCheck.isChecked() ? "1" : "0");
        ((GlopalParams) this.getApplication()).setKykMebCheck(kykMebCheck.isChecked() ? "1" : "0");
        ((GlopalParams) this.getApplication()).setFrizCheck(frizCheck.isChecked() ? "1" : "0");
        ((GlopalParams) this.getApplication()).setBalconCheck(balconCheck.isChecked() ? "1" : "0");
        ((GlopalParams) this.getApplication()).setInetCheck(inetCheck.isChecked() ? "1" : "0");
        ((GlopalParams) this.getApplication()).setKondicCheck(kondicCheck.isChecked() ? "1" : "0");
        ((GlopalParams) this.getApplication()).setWoshMashinCheck(woshMashinCheck.isChecked() ? "1" : "0");

        String areasIDsort = getValueID(Integer.parseInt(areasID),"a.id_area");
        String durationsIDsort = getValueID(Integer.parseInt(durationsID),"id_duration");
        String regeonIDsort = getValueID(Integer.parseInt(regeonID),"a.id_region");
        String typeRealatyIDsort  = getValueID(Integer.parseInt(typeRealatyID),"id_type_realty");
        String typeHome_IDsort = getValueID(Integer.parseInt(typeHome_ID),"id_type_house");
        String typeRemIDsort = getValueID(Integer.parseInt(typeRemID),"id_type_upkeep");
        String typePlanIDsort = getValueID(Integer.parseInt(typePlanID),"id_type_layout");



        String query =  "" +rumCount + "" +atajCount+ "" +ataj+ "" +ouerArea+ "" +liveArea+ "" +kykArea+ "" +price+
                ""+mebVel+""+kykMebVel+""+frizVel+""+balconVel+""+inetVel+""+kondicVel+""+woshMashinVel+""+areasIDsort+""+durationsIDsort+""+regeonIDsort
                +""+typeRealatyIDsort+""+typeHome_IDsort+""+typeRemIDsort+""+typePlanIDsort;
        return query;
    }
    private void Search()
    {
        answerIntent = new Intent();


        //answerIntent.putExtra("RESULT", "789456123");
        answerIntent.putExtra("RESULT", getSerch());
        setResult(RESULT_OK, answerIntent);
        finish();
    }
}
