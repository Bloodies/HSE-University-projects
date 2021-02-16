package org.hse.android;

import androidx.appcompat.app.AppCompatActivity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

import java.util.Date;

public class MainActivity extends AppCompatActivity {

    public Date currentTime;
    private static final String TAG = "MainActivity";

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        getSupportActionBar().hide();

        View button_student = findViewById(R.id.button_student);
        View button_teacher = findViewById(R.id.button_teacher);
        View button_settings = findViewById(R.id.button_settings);

        button_student.setOnClickListener(new View.OnClickListener() {
            @Override public void onClick(View v) {
                //Intent intent = new Intent(MainActivity.this, StudentActivity.class);
                //startActivity(intent);
                Student_pressed();
            }
        });
        button_teacher.setOnClickListener(new View.OnClickListener() {
            @Override public void onClick(View v) {
                //Intent intent = new Intent(MainActivity.this, TeacherActivity.class);
                //startActivity(intent);
                Teacher_pressed();
            }
        });
        button_settings.setOnClickListener(new View.OnClickListener() {
            @Override public void onClick(View v) {
                //Intent intent = new Intent(MainActivity.this, StudentActivity.class);
                //startActivity(intent);
                Settings_pressed();
            }
        });
    }
    private void Student_pressed(){
        Intent intent = new Intent(MainActivity.this, StudentActivity.class);
        startActivity(intent);
    }
    private void Teacher_pressed(){
        Intent intent = new Intent(MainActivity.this, TeacherActivity.class);
        startActivity(intent);
    }
    private void Settings_pressed(){
        Intent intent = new Intent(MainActivity.this, SettingsActivity.class);
        startActivity(intent);
    }
}