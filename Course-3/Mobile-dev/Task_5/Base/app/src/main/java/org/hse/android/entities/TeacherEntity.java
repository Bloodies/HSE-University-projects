package org.hse.android.entities;

import androidx.annotation.NonNull;
import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.Index;
import androidx.room.PrimaryKey;

@Entity(tableName = "teacher", indices = {@Index(value = {"fio"}, unique = true)})
public class TeacherEntity {
    @PrimaryKey
    public int id;

    @ColumnInfo(name = "fio")
    @NonNull
    public String fio = "";
}