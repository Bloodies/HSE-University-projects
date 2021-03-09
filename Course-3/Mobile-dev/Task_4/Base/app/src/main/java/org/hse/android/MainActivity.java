package org.hse.android;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import androidx.appcompat.app.AppCompatActivity;
import java.util.Objects;

public class MainActivity extends AppCompatActivity {

    private static final String TAG = "MainActivity";

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Objects.requireNonNull(getSupportActionBar()).hide();

        View button_student = findViewById(R.id.button_student);
        View button_teacher = findViewById(R.id.button_teacher);
        View button_settings = findViewById(R.id.button_settings);

        Log.d(TAG, "Startup");

        button_student.setOnClickListener(v -> Student_pressed());
        button_teacher.setOnClickListener(v -> Teacher_pressed());
        button_settings.setOnClickListener(v -> Settings_pressed());
    }

    private void Student_pressed(){
        Intent intent = new Intent(this, StudentActivity.class);
        startActivity(intent);
    }

    private void Teacher_pressed(){

        Intent intent = new Intent(this, TeacherActivity.class);
        startActivity(intent);
    }

    private void Settings_pressed(){
        Intent intent = new Intent(this, SettingsActivity.class);
        startActivity(intent);
    }
}