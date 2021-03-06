package org.hse.android;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import androidx.lifecycle.ViewModelProviders;
import org.hse.android.database.Group;
import org.hse.android.entities.TeacherEntity;
import org.hse.android.entities.TimeTableEntity;
import org.hse.android.entities.TimeTableWithTeacherEntity;
import org.hse.android.models.MainViewModel;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.Objects;

public class TeacherActivity extends BaseActivity {
    protected MainViewModel mainViewModel;

    private static final String TAG = "TeacherActivity";

    private TextView status, subject, cabinet, corp, teacher, time_start, time_end, type_subj;
    private Spinner spinner_teacher;
    public Date currentTime;
    ArrayAdapter<Group> adapter;

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_teacher);
        Objects.requireNonNull(getSupportActionBar()).hide();
        mainViewModel = ViewModelProviders.of(this).get(MainViewModel.class);

        spinner_teacher = findViewById(R.id.teacherList);

        List<Group> groups = new ArrayList<>();
        initGroupList(groups);

        adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, groups);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

        spinner_teacher.setAdapter(adapter);

        spinner_teacher.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            public void onItemSelected(AdapterView<?> parent, View itemSelected, int selectedItemPosition, long selectedId) {
                Object item = adapter.getItem(selectedItemPosition);
                showTime(currentTime);
                Log.d(TAG,"selectedItem: " + item);
            }
            public void onNothingSelected(AdapterView<?> parent) { }
        });

        initTime();

        time_start = findViewById(R.id.start);
        time_end = findViewById(R.id.end);
        type_subj = findViewById(R.id.type);
        status = findViewById(R.id.status);
        subject = findViewById(R.id.name);
        cabinet = findViewById(R.id.place);
        corp = findViewById(R.id.corp);
        teacher = findViewById(R.id.teacher);
        initData();

        View scheduleDay = findViewById(R.id.schedule_day);
        scheduleDay.setOnClickListener(v -> showSchedule(ScheduleType.DAY));
        View scheduleWeek = findViewById(R.id.schedule_week);
        scheduleWeek.setOnClickListener(v -> showSchedule(ScheduleType.WEEK));
    }

    private void initData() { initDataFromTimeTable(null); }

    @Override
    public void showTime(Date currentTime) {
        super.showTime(currentTime);
        mainViewModel.getTimeTableTeacherByDate(currentTime).observe(this, list -> {
            for (TimeTableWithTeacherEntity listEntity : list) {
                Log.d(TAG, listEntity.timeTableEntity.subjName + " " + listEntity.teacherEntity.fio);
                if (getSelectedGroup() != null && getSelectedGroup().getId().equals(listEntity.timeTableEntity.teacherId)) {
                    initDataFromTimeTable(listEntity);
                }
            }
        });
    }

    private void initGroupList(List<Group> groups){
        mainViewModel.getTeachers().observe(this, list -> {
            List<Group> groupsResult = new ArrayList<>();
            for (TeacherEntity listEntity : list) {
                groupsResult.add(new Group(listEntity.id, listEntity.fio));
            }
            adapter.clear();
            adapter.addAll(groupsResult);
        });
    }

    private void showSchedule(ScheduleType type) {
        Object selectedItem = spinner_teacher.getSelectedItem();
        if (!(selectedItem instanceof Group)) { return; }
        showScheduleImpl(type, (Group) selectedItem, currentTime);
    }

    protected void showScheduleImpl(ScheduleType type, Group group, Date currentTime) {
        Intent intent = new Intent(this, ScheduleActivity.class);
        intent.putExtra(ScheduleActivity.ARG_NAME, group.getName());
        intent.putExtra(ScheduleActivity.ARG_ID, group.getId());
        intent.putExtra(ScheduleActivity.ARG_TYPE, type);
        intent.putExtra(ScheduleActivity.ARG_MODE, ScheduleMode.TEACHER);
        intent.putExtra(ScheduleActivity.ARG_TIME, currentTime);
        startActivity(intent);
    }

    @SuppressLint("SetTextI18n")
    private void initDataFromTimeTable(TimeTableWithTeacherEntity timeTableTeacherEntity) {
        if (timeTableTeacherEntity == null) {
            time_start.setText("00:00");
            time_end.setText("00:00");
            status.setText("Нет пар");

            type_subj.setText("");
            subject.setText("Дисциплина");
            cabinet.setText("Кабинет");
            corp.setText("Корпус");
            teacher.setText("Преподаватель");
            return;
        }
        status.setText("Идет пара");
        TimeTableEntity timeTableEntity = timeTableTeacherEntity.timeTableEntity;

        time_start.setText(formatToMinutes(timeTableTeacherEntity.timeTableEntity.timeStart));
        time_end.setText(formatToMinutes(timeTableTeacherEntity.timeTableEntity.timeEnd));
        type_subj.setText(timeTableEntity.type);
        subject.setText(timeTableEntity.subjName);
        cabinet.setText("Ауд. " + timeTableEntity.cabinet);
        corp.setText("Корп. " + timeTableEntity.corp);
        teacher.setText("Преп. " + timeTableTeacherEntity.teacherEntity.fio);
    }

    private String formatToMinutes(Date date){
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("HH:mm", Locale.getDefault());
        return simpleDateFormat.format(date);
    }

    protected Group getSelectedGroup(){
        return (Group) spinner_teacher.getSelectedItem();
    }
}