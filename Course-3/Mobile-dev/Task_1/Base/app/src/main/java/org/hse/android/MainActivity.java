package org.hse.android;

import androidx.appcompat.app.AppCompatActivity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        View button_student = findViewById(R.id.button_student);
        View button_teacher = findViewById(R.id.button_teacher);

        button_student.setOnClickListener(new View.OnClickListener() {
            @Override public void onClick(View v) {
                Intent intent = new Intent(this, StudentActivity.class);
                startActivity(intent);
            }
        });
        button_teacher.setOnClickListener(new View.OnClickListener() {
            @Override public void onClick(View v) { Toast.makeText(MainActivity.this, "РАСПИСАНИЕ ДЛЯ ПРЕПОДОВАТЕЛЯ", Toast.LENGTH_SHORT).show(); }
        });
    }
}