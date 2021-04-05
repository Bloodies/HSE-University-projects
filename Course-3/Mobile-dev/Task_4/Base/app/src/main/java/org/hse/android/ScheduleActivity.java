package org.hse.android;

import android.annotation.SuppressLint;
import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import org.hse.android.entities.TimeTableWithTeacherEntity;
import org.hse.android.models.MainViewModel;
import org.hse.android.models.ScheduleItem;
import org.hse.android.models.ScheduleItemHeader;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import java.text.DateFormatSymbols;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Objects;
import java.util.stream.Collectors;

public class ScheduleActivity extends AppCompatActivity {
    protected MainViewModel mainViewModel;

    private BaseActivity.ScheduleType type;
    private BaseActivity.ScheduleMode mode;

    public RecyclerView recyclerView;
    public ItemAdapter adapter;

    static public String ARG_NAME = "0", ARG_ID = "1", ARG_TYPE = "2", ARG_MODE = "3", ARG_TIME = "4", name;
    static public Integer DEFAULT_ID = 0, id;
    private TextView currentTime;

    List<ScheduleItem> scheduleList = new ArrayList<>();

    interface OnItemClick { void onClick(ScheduleItem data); }

    private DateFormatSymbols DaySymbols() {
        String[] Week_days = { "", "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Субота" };
        DateFormatSymbols symbols = new DateFormatSymbols( new Locale("ru", "ru"));
        symbols.setShortWeekdays(Week_days);
        return symbols;
    }

    private String formatDay(Date date) {
        @SuppressLint("SimpleDateFormat")
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("E, dd MMMM", DaySymbols());
        return simpleDateFormat.format(date);
    }

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_schedule);
        Objects.requireNonNull(getSupportActionBar()).hide();
        mainViewModel = ViewModelProviders.of(this).get(MainViewModel.class);

        @SuppressLint("SimpleDateFormat")
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("E, dd MMMM", DaySymbols());
        currentTime = findViewById(R.id.current_time);
        currentTime.setText(String.format("%s", simpleDateFormat.format(BaseActivity.time_export)));

        type = (BaseActivity.ScheduleType) getIntent().getSerializableExtra(ARG_TYPE);
        mode = (BaseActivity.ScheduleMode) getIntent().getSerializableExtra(ARG_MODE);
        id = getIntent().getIntExtra(ARG_ID, DEFAULT_ID);

        TextView schedule_title = findViewById(R.id.schedule_title);
        schedule_title.setText(getIntent().getStringExtra(ARG_NAME));

        recyclerView = findViewById(R.id.listView);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        recyclerView.addItemDecoration(new DividerItemDecoration(this, LinearLayoutManager.VERTICAL));
        recyclerView.setHasFixedSize(true);

        adapter = new ItemAdapter(data -> { });
        recyclerView.setAdapter(adapter);

        initData();
    }

    private void initData(){
        Observer observer = (Observer<List<TimeTableWithTeacherEntity>>) timeTableWithTeacherEntities -> {
            scheduleList = getScheduleItems(timeTableWithTeacherEntities);
            adapter.setDataList(scheduleList);
            recyclerView.setAdapter(adapter);
        };
        applyFunctionForTimeTable(observer);
    }

    private void applyFunctionForTimeTable(Observer observer) {
        switch (type){
            case DAY:
                switch (mode){
                    case STUDENT:
                        mainViewModel.getTimeTableForStudentDay(BaseActivity.time_export, id).observe(this, observer);
                        break;
                    case TEACHER:
                        mainViewModel.getTimeTableForTeacherDay(BaseActivity.time_export, id).observe(this, observer);
                        break;
                }
                break;
            case WEEK:
                switch (mode){
                    case STUDENT:
                        mainViewModel.getTimeTableForStudentWeek(BaseActivity.time_export, id).observe(this, observer);
                        break;
                    case TEACHER:
                        mainViewModel.getTimeTableForTeacherWeek(BaseActivity.time_export, id).observe(this, observer);
                        break;
                }
                break;
        }
    }

    private List<ScheduleItem> getScheduleItems(List<TimeTableWithTeacherEntity> timeTableWithTeacherEntities) {
        List<ScheduleItem> list = new ArrayList<>();

        Map<String, List<TimeTableWithTeacherEntity>> days =
                null;
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.N) {
            days = timeTableWithTeacherEntities
                    .stream()
                    .sorted(Comparator.comparing(t -> t.timeTableEntity.timeStart))
                    .collect(Collectors.groupingBy(t -> formatDay(t.timeTableEntity.timeStart)));
        }

        for (Map.Entry<String, List<TimeTableWithTeacherEntity>> day: days.entrySet()
        ) {
            ScheduleItemHeader header = new ScheduleItemHeader();
            header.setTitle(day.getKey());
            list.add(header);

            for(TimeTableWithTeacherEntity timetable: day.getValue()){
                list.add(convertItem(timetable));
            }
        }
        return list;
    }

    private String formatToMinutes(Date date){
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("HH:mm", Locale.getDefault());
        return simpleDateFormat.format(date);
    }

    private ScheduleItem convertItem(TimeTableWithTeacherEntity timeTableWithTeacherEntity){
        ScheduleItem item = new ScheduleItem();
        item.setStart(formatToMinutes(timeTableWithTeacherEntity.timeTableEntity.timeStart));
        item.setEnd(formatToMinutes(timeTableWithTeacherEntity.timeTableEntity.timeEnd));
        item.setType(timeTableWithTeacherEntity.timeTableEntity.type);
        item.setName(timeTableWithTeacherEntity.timeTableEntity.subjName);
        item.setPlace("Корп. " + timeTableWithTeacherEntity.timeTableEntity.corp);
        item.setTeacher("Преп. " + timeTableWithTeacherEntity.teacherEntity.fio);
        return item;
    }

    public final static class ItemAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {

        private final static int TYPE_ITEM = 0;
        private final static int TYPE_HEADER = 1;

        private List<ScheduleItem> dataList = new ArrayList<>();
        private OnItemClick onItemClick;

        public ItemAdapter(OnItemClick onItemClick) { this.onItemClick = onItemClick; }

        @NonNull
        @Override
        public RecyclerView.ViewHolder onCreateViewHolder(@NotNull ViewGroup parent, int viewType) {
            Context context = parent.getContext();
            LayoutInflater inflater = LayoutInflater.from(context);

            if (viewType == TYPE_ITEM) {
                View contactView = inflater.inflate(R.layout.item_schedule, parent, false);
                return new ViewHolder(contactView, context, onItemClick);
            }
            else if (viewType == TYPE_HEADER) {
                View contactView = inflater.inflate(R.layout.item_schedule_header, parent, false);
                return new ViewHolderHeader(contactView, context, onItemClick);
            }
            throw new IllegalArgumentException("Invalid view type");
        }

        public int getItemViewType(int position) {
            ScheduleItem data = dataList.get(position);
            if (data instanceof ScheduleItemHeader) { return TYPE_HEADER; }
            return TYPE_ITEM;
        }

        public void setDataList(List<ScheduleItem> list) {
            this.dataList = new ArrayList<>();
            if (dataList != null) { this.dataList.addAll(list); }
            notifyDataSetChanged();
        }

        @Override
        public void onBindViewHolder(@NotNull RecyclerView.ViewHolder viewHolder, int position) {
            ScheduleItem data = dataList.get(position);
            if (viewHolder instanceof ViewHolder) {
                ((ViewHolder) viewHolder).bind(data);
            }
            else if (viewHolder instanceof ViewHolderHeader) {
                ((ViewHolderHeader) viewHolder).bind((ScheduleItemHeader) data);
            }
        }

        @Override
        public int getItemCount() {
            return dataList.size();
        }
    }

    public static class ViewHolder extends RecyclerView.ViewHolder {
        private Context context;
        private OnItemClick onItemClick;
        private TextView start, end, type, name, place, teacher;

        public ViewHolder(View itemView, Context context,OnItemClick onItemClick) {
            super(itemView);
            this.context = context;
            this.onItemClick = onItemClick;
            start = itemView.findViewById(R.id.start);
            end = itemView.findViewById(R.id.end);
            type = itemView.findViewById(R.id.type);
            name = itemView.findViewById(R.id.name);
            place = itemView.findViewById(R.id.place);
            teacher = itemView.findViewById(R.id.teacher);
        }

        public void bind(final ScheduleItem data) {
            start.setText(data.getStart());
            end.setText(data.getEnd());
            type.setText(data.getType());
            name.setText(data.getName());
            place.setText(data.getPlace());
            teacher.setText(data.getTeacher());
        }
    }

    public static class ViewHolderHeader extends RecyclerView.ViewHolder {
        private Context context;
        private OnItemClick onItemClick;
        private TextView title;

        public ViewHolderHeader(View itemView, Context context, OnItemClick onItemClick) {
            super(itemView);
            this.context = context;
            this.onItemClick = onItemClick;
            title = itemView.findViewById(R.id.title);
        }

        public void bind(final ScheduleItemHeader data) { title.setText(data.getTitle()); }
    }
}