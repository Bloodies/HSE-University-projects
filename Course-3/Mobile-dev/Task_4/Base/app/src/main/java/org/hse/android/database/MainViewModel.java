package org.hse.android.database;

import android.app.Application;
import android.content.Context;
import androidx.annotation.NonNull;
import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;
import java.util.Date;
import java.util.List;

public class MainViewModel extends AndroidViewModel {
    private HseRepository repository;

    public MainViewModel(@NonNull Application application) {
        super(application);
        repository = new HseRepository(application);
    }

    public LiveData<List<GroupEntity>> getGroups() { return repository.getGroups(); }

    public LiveData<List<TeacherEntity>> getTeachers() { return repository.getTeachers(); }

    public LiveData<List<TimeTableWithTeacherEntity>> getTimeTableTeacherByDate(Date date) {
        return repository.getTimeTableTeacherByDate(date);
    }
}

