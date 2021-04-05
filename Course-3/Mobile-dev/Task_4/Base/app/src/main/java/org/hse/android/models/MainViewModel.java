package org.hse.android.models;

import android.app.Application;
import androidx.annotation.NonNull;
import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import org.hse.android.database.HseRepository;
import org.hse.android.entities.GroupEntity;
import org.hse.android.entities.TeacherEntity;
import org.hse.android.entities.TimeTableWithTeacherEntity;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;

public class MainViewModel extends AndroidViewModel {
    private HseRepository repository;

    public MutableLiveData<Date> currentTime;

    public MainViewModel(@NonNull Application application){
        super(application);
        repository = new HseRepository(application);
        currentTime = new MutableLiveData<Date>();
    }

    public LiveData<List<GroupEntity>> getGroups() {
        return repository.getGroups();}

    public LiveData<List<TeacherEntity>> getTeachers() {
        return repository.getTeachers();}

    public LiveData<List<TimeTableWithTeacherEntity>> getTimeTableTeacherByDate(Date date) {
        return repository.getTimeTableTeacherByDate(date);
    }

    public LiveData<TimeTableWithTeacherEntity> getTimeWithTeacherByDate(Date date, int teacherId) {
        return repository.getTimeWithTeacherByDate(date, teacherId);
    }

    public LiveData<TimeTableWithTeacherEntity> getTimeWithGroupByDate(Date date, int groupId) {
        return repository.getTimeWithGroupByDate(date, groupId);
    }

    public LiveData<List<TimeTableWithTeacherEntity>> getTimeTableForStudentDay(Date date, Integer groupId) {
        Date start = floorDay(date);
        Date end = ceilDay(date);
        return repository.getTimeWithGroupByDateRange(start, end, groupId);
    }

    public LiveData<List<TimeTableWithTeacherEntity>> getTimeTableForTeacherDay(Date date, Integer teacherId) {
        Date start = floorDay(date);
        Date end = ceilDay(date);
        return repository.getTimeWithTeacherByDateRange(start, end, teacherId);
    }

    public LiveData<List<TimeTableWithTeacherEntity>> getTimeTableForStudentWeek(Date date, Integer groupId) {
        Date start = floorDay(date);
        Date end = ceilWeek(date);
        return repository.getTimeWithGroupByDateRange(start, end, groupId);
    }

    public LiveData<List<TimeTableWithTeacherEntity>> getTimeTableForTeacherWeek(Date date, Integer teacherId) {
        Date start = floorDay(date);
        Date end = ceilWeek(date);
        return repository.getTimeWithTeacherByDateRange(start, end, teacherId);
    }

    private Date floorDay(Date date){
        Calendar c = new GregorianCalendar();
        c.setTime(date);
        c.set(Calendar.HOUR_OF_DAY, 0);
        c.set(Calendar.MINUTE, 0);
        c.set(Calendar.SECOND, 0);

        return c.getTime();
    }

    private Date ceilDay(Date date){
        Calendar c = new GregorianCalendar();
        c.setTime(date);

        c.set(Calendar.HOUR_OF_DAY, 23);
        c.set(Calendar.MINUTE, 59);
        c.set(Calendar.SECOND, 59);

        return c.getTime();
    }

    private Date ceilWeek(Date date){
        Calendar c = new GregorianCalendar();
        c.setTime(date);
        c.setFirstDayOfWeek(Calendar.MONDAY);

        c.set(Calendar.DAY_OF_WEEK, Calendar.SUNDAY);
        c.set(Calendar.HOUR_OF_DAY, 23);
        c.set(Calendar.MINUTE, 59);
        c.set(Calendar.SECOND, 59);

        return c.getTime();
    }
}