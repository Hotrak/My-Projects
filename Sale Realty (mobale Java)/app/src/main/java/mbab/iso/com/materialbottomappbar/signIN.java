package mbab.iso.com.materialbottomappbar;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.material.bottomappbar.BottomAppBar;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Date;

public class signIN extends AppCompatActivity {

    EditText login;
    EditText pass;

    Button buttonSignIn;
    TextView newAccauntBtn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_in);
//        getWindow().setFlags(WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS,WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);

        login = findViewById(R.id.loginSignIn);
        pass = findViewById(R.id.passIn);

        buttonSignIn = findViewById(R.id.buttonSignIn);
        newAccauntBtn = findViewById(R.id.newAccaundBtn);


        buttonSignIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(SignIn())
                {
                    Intent questionIntent = new Intent(signIN.this, MainActivity.class);
                    startActivity(questionIntent);
                }else
                    Message("Не верный логин или пароль");
            }
        });

        newAccauntBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent questionIntent = new Intent(signIN.this, sign_up.class);
                startActivity(questionIntent);
            }
        });

        TextView textView = findViewById(R.id.textView2);

        textView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Uri uri = Uri.parse("http://bomj.malaha.beget.tech/newpass.php"); // missing 'http://' will cause crashed
                Intent intent = new Intent(Intent.ACTION_VIEW, uri);
                startActivity(intent);
            }
        });


    }
    public void Message(String message) {

        Toast.makeText(this, message, Toast.LENGTH_LONG).show();
    }
    private boolean SignIn()
    {
        zapros Zaprps = new zapros("http://bomj.malaha.beget.tech/Home/SignIn.php");

            String query = login.getText()+"<>"+pass.getText() ;
        Zaprps.start(query);

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


        if(error ==1)
        {
            try {
                saveText(jo.getString("id"),login.getText().toString());
            } catch (JSONException e) {
                e.printStackTrace();
            }
            Log.e("SecsesInsert",error+"");

        }
        else
            Log.e("ErrorInsert",error+"");


        return  error ==1;
    }

    public void saveText(String text,String login){

        FileOutputStream fos = null;
        try {

            fos = openFileOutput("content.txt", MODE_PRIVATE);
            String result = text +"-"+login;

            fos.write(result.getBytes());
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
    public void FinishActivity()
    {
        getIntent().getSerializableExtra("EXTRA_MESSAGE").toString();
    }
}
