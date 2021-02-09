package org.hse.android;

import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;

public class StudentActivity extends AppCompatActivity {

    private static final String LOG_CODE = "LOG_CODE";
    private TextView time, status, subject, cabinet, corp, teacher;
    public Date currentTime;

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_student);
        //getSupportActionBar().hide();

        final Spinner spinner = findViewById(R.id.groupList);

        List<Group> groups = new ArrayList<>();
        initGroupList(groups);

        ArrayAdapter<?> adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, groups);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

        spinner.setAdapter(adapter);

        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            public void onItemSelected(AdapterView<?> parent, View itemSelected, int selectedItemPosition, long selectedId) {
                Object item = adapter.getItem(selectedItemPosition);
                Log.d("Status","selectedItem: " + item);
            }
            public void onNothingSelected(AdapterView<?> parent) { }
        });

        time = findViewById(R.id.time);
        initTime();

        status = findViewById(R.id.status);
        subject = findViewById(R.id.subject);
        cabinet = findViewById(R.id.cabinet);
        corp = findViewById(R.id.corp);
        teacher = findViewById(R.id.teacher);

        initData();
    }
    private void initGroupList(List<Group> groups){
        groups.add(new Group(1,"ПИ-18-1"));
        groups.add(new Group(2,"ПИ-18-2"));
    }
    private void initTime(){
        currentTime = new Date();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("HH:mm", Locale.getDefault());
        SimpleDateFormat simpleDayFormat = new SimpleDateFormat("E", Locale.getDefault());
        //switch (simpleDayFormat.toString()){
        //    case
        //}
        //if(simpleDayFormat.toString() == "st")
        //String date = System.out.printf("%1$R %2$A", "Дата:", currentTime).toString;
        //time.setText("%1$R %2$A", currentTime);
        time.setText(simpleDateFormat.format(currentTime));
    }
    private void initData(){
        status.setText("Нет пар");

        subject.setText("Дисциплина");
        cabinet.setText("Кабинет");
        corp.setText("Корпус");
        teacher.setText("Преподователь");
    }
    static class Group{
        private Integer id;
        private String name;

        public Group(Integer id, String name){
            this.id = id;
            this.name = name;
        }
        public Integer getId(){ return id; }
        public void setId(Integer id) { this.id = id; }
        @Override public String toString() { return name; }
        public String getName() { return name; }
        public void setName(String name){ this.name = name; }
    }
}