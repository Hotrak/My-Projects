package mbab.iso.com.materialbottomappbar;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.AppCompatEditText;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.WindowManager;
import android.widget.AbsListView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Date;

public class sign_up extends AppCompatActivity {

    String eMail;
    String login;
    String pass1;
    String pass2;
    String tel;
    Button signUpButton;

    EditText firstName;

    //1 - агенство
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_up);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS,WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);


        firstName = ((EditText) findViewById(R.id.upEmail));

        firstName.addTextChangedListener(new TextWatcher()  {

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
            }

            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void afterTextChanged(Editable s)  {
                if (firstName.getText().toString().length() <= 0) {
                    firstName.setError("Enter FirstName");
                } else {
                    firstName.setError(null);
                }
            }
        });

        signUpButton = findViewById(R.id.buttonUP);
        signUpButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                eMail = ((EditText) findViewById(R.id.upEmail)).getText().toString();
                login = ((EditText) findViewById(R.id.upLogin)).getText().toString();
                pass1 = ((EditText) findViewById(R.id.upPass1)).getText().toString();
                pass2 = ((EditText) findViewById(R.id.upPass2)).getText().toString();
                tel = ((EditText) findViewById(R.id.upPhone)).getText().toString();
                if(SignUp())
                {

                    finish();
                }

            }
        });

        TextView textView = findViewById(R.id.signIN);
        textView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent questionIntent = new Intent(sign_up.this, signIN.class);
                startActivity(questionIntent);
            }
        });


    }


    private boolean SignUp()
    {
        zapros Zaprps = new zapros("http://bomj.malaha.beget.tech/Home/SignUp.php");

        String query = "INSERT INTO users (name,phone,email,password,coin,creation_date) VALUES ('"+login+"','"+tel+"','"+eMail+"','"+pass1+"','0','<@>')<>"+pass1;
        Log.e("SIGN_UP: ", query);
        Zaprps.start(query);

        try {
            Zaprps.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }



        return true;
    }

    private void CheckValue()
    {

    }

}
