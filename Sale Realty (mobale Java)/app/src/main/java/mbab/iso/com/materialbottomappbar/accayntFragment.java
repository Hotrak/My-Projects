package mbab.iso.com.materialbottomappbar;


import android.content.Intent;
import android.graphics.Typeface;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.os.StrictMode;
import android.text.SpannableString;
import android.text.style.StyleSpan;
import android.text.style.UnderlineSpan;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;


/**
 * A simple {@link Fragment} subclass.
 */
public class accayntFragment extends Fragment {

    String[] counts;
    public accayntFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {


        return inflater.inflate(R.layout.fragment_accaynt, container, false);
    }
    TextView loginTex;
    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        TextView loginText = (TextView) getView().findViewById(R.id.login);
        SpannableString spanString = new SpannableString(((GlopalParams) getActivity().getApplication()).getLogin());
        spanString.setSpan(new StyleSpan(Typeface.BOLD), 0, spanString.length(), 0);
        loginText.setText(spanString);
        //Exit();

    }

    public void LoadData(String result)
    {
        JSONObject jo = null;
        try {
            jo = new JSONArray(result).getJSONObject(0);
        } catch (JSONException e) {
            e.printStackTrace();
        }
        counts = new String[5];
        try {
            counts[0] = jo.getString("coin");
            counts[1] = jo.getString("name");
            counts[2] = jo.getString("phone");
            counts[3] = jo.getString("cop");
            counts[4] = jo.getString("want");
        } catch (JSONException e) {
            e.printStackTrace();
        }

        int[] imgs= new int[] {
                R.drawable.ic_dollar,
                R.drawable.user,
                R.drawable.ic_call,
                R.drawable.ic_layers,
                R.drawable.ic_heart
        };

        final String[] catNames = new String[] {
                "Баланс","Имя","Телефон",  "Мои объявления", "Мои желания"
        };

        ListView listView = (ListView) getView().findViewById(R.id.listV);

        accauntListViewAdapter customListView=new accauntListViewAdapter(getActivity(),catNames,counts,imgs);
        listView.setAdapter(customListView);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

            if(position==3)
            {
                Intent questionIntent = new Intent(getContext(), myAdsActivity.class);
                startActivity(questionIntent);
            }
            if(position==4&&!counts[3].equals("0"))
            {
                ((MainActivity) getActivity()).setBlanckFragment();
            }

            }
        });

        ImageView exitButton = (ImageView) getView().findViewById(R.id.exitButton);

        exitButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Exit();
            }
        });
    }
    public void Exit()
    {
        ((GlopalParams) getActivity().getApplication()).setId("-1");
        ((GlopalParams) getActivity().getApplication()).setLogin("-1");
        Intent questionIntent = new Intent(getActivity(), signIN.class);
        startActivity(questionIntent);
        getContext().deleteFile("content.txt");
    }

}
