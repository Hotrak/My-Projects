package mbab.iso.com.materialbottomappbar;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;


public class listActivity extends AppCompatActivity {
    ListView listView;
    String tag;

    ArrayList<String> list;
    //String[] list;
    ArrayList<String>  idData;

    String id = "-1";
    String[] idRegion;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_list);

        String data = getIntent().getSerializableExtra("LIST").toString();

        if(getIntent().getSerializableExtra("ID")!=null)
        {
            id = getIntent().getSerializableExtra("ID").toString();
        }

        tag = getIntent().getSerializableExtra("TAG").toString();
        list = new ArrayList<>();
        idData = new ArrayList<>();

        list = GetArrayValue(data);

        listView = findViewById(R.id.lv);
        StrictMode.setThreadPolicy((new StrictMode.ThreadPolicy.Builder().permitNetwork().build()));

        listItemAdapter customListView=new listItemAdapter(this,list);
        listView.setAdapter(customListView);

        answerIntent = new Intent();
        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                answerIntent.putExtra("RESULT", list.get(position));
                answerIntent.putExtra("RESULT_ID", idData.get(position));
                setResult(RESULT_OK, answerIntent);
                finish();
            }
        });
    }

    private ArrayList<String> GetArrayValue(String data)
    {

        JSONArray ja = null;
        JSONObject jo;
        try {
            ja = new JSONArray(data);
            jo = null;

        }catch (JSONException e) {

        }
        for (int i = 0; i <= ja.length(); i++) {

            try {
                jo = ja.getJSONObject(i);


                if(id!="-1")
                {
                    if(Integer.parseInt(id)==Integer.parseInt(jo.getString("id_"+"area")) )
                    {
                        idData.add(jo.getString("id_"+tag));
                        list.add(jo.getString(tag));
                    }
                }else
                    {
                        idData.add(jo.getString("id_"+tag));
                        list.add(jo.getString(tag));
                    }
            } catch (JSONException e) {

            }

        }


        return list;
    }
    Intent answerIntent;
}
