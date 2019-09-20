package mbab.iso.com.materialbottomappbar;

import android.util.Log;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;

public class zapros extends Thread
{
    String id;
    String surname = "111";
    String name = "111";
    String middlename = "111";
    InputStream is = null;
    String result = null;
    String line = null;

    String url;

    public zapros(String url)
    {
        this.url = url;
    }
    public void run()
    {
        try {
            GetText();
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
//
    }

    // принемаем id при запуске потока
    public void start(String idp)
    {
        this.id = idp;
        this.start();
    }


    public void GetText()throws UnsupportedEncodingException {

        String Message = id.toString();
        String Email = "v";

        String data = URLEncoder.encode("Query", "UTF-8")
                + "=" + URLEncoder.encode(Message, "UTF-8");

        data += "&" + URLEncoder.encode("email", "UTF-8") + "="
                + URLEncoder.encode(Email, "UTF-8");

        String text = "";
        BufferedReader reader=null;
        HttpURLConnection urlConnection = null;

        try
        {

            // Defined URL  where to send data
            URL url = new URL(this.url);

            // Send POST data request


            urlConnection = (HttpURLConnection) url.openConnection();
            urlConnection.setRequestMethod("POST");
            urlConnection.setDoOutput(true);
            OutputStreamWriter wr = new OutputStreamWriter(urlConnection.getOutputStream());
            wr.write( data );
            wr.flush();

            // Get the server response

            reader = new BufferedReader(new InputStreamReader(urlConnection.getInputStream()));
            StringBuilder sb = new StringBuilder();
            String line = null;

            // Read Server Response
            while((line = reader.readLine()) != null)
            {
                // Append server response in string
                sb.append(line + "\n");
            }


            text = sb.toString();
        }
        catch(Exception ex)
        {
        }
        finally
        {
            try
            {

                reader.close();
            }

            catch(Exception ex) {}
        }

        // Show response on activity

        //content.setText( text  );
        result = text;
//        name = result;
//        // обрабатываем полученный json
//        try
//        {
//            JSONObject json_data = new JSONObject(result);
//            surname = (json_data.getString("Surname"));
//            name=(json_data.getString("Name"));
//            middlename=(json_data.getString("Middlename"));
//            Log.e("pass 3",name);
//        }
//        catch(Exception e)
//        {
//            Log.e("Fail 3", e.toString());
//        }


    }
    public String resname ()
    {
        return  name;
    }
    public String ressurname ()
    {
        return  surname;
    }
    public String resmiddlename ()
    {
        return  middlename;
    }
    public String result(){return result;}
}
