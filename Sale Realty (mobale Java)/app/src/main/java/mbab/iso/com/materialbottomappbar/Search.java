package mbab.iso.com.materialbottomappbar;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.Menu;

import com.google.android.material.bottomnavigation.BottomNavigationView;

public class Search extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);

        BottomNavigationView bnv = (BottomNavigationView)findViewById(R.id.bottom_navigation_view);
        Menu menu = bnv.getMenu();

        menu.getItem(0).setTitle(null);
        menu.getItem(1).setTitle(null);
        menu.getItem(2).setTitle(null);
    }
}
