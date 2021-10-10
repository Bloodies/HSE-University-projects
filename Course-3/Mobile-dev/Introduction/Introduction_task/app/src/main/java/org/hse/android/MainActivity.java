package org.hse.android;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;

public class MainActivity extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Log.d("STRING", "Строчка с моим именем и фамилией: Ануфриев Павел");
        setContentView(R.layout.activity_main);
    }
}