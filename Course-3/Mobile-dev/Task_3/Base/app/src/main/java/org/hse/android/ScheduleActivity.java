package org.hse.android;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import org.hse.android.cfg.ScheduleItem;
import org.hse.android.cfg.ScheduleItemHeader;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.Objects;

public class ScheduleActivity extends AppCompatActivity {
    private BaseActivity.ScheduleType type;
    private BaseActivity.ScheduleMode mode;

    public RecyclerView recyclerView;
    public ItemAdapter adapter;

    static public String ARG_NAME = "0", ARG_ID = "1", ARG_TYPE = "2", ARG_MODE = "3", ARG_TIME = "4", name;
    static public Integer DEFAULT_ID = 0, id;
    private TextView currentTime;

    interface OnItemClick { void onClick(ScheduleItem data); }

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_schedule);
        Objects.requireNonNull(getSupportActionBar()).hide();

        SimpleDateFormat simpleDateFormat = null;
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.LOLLIPOP) {
            simpleDateFormat = new SimpleDateFormat("EEEE, dd MMMM", Locale.forLanguageTag("ru"));
        }
        currentTime = findViewById(R.id.current_time);
        currentTime.setText(String.format("%s", simpleDateFormat.format(BaseActivity.time_export)));

        type = (BaseActivity.ScheduleType) getIntent().getSerializableExtra(ARG_TYPE);
        mode = (BaseActivity.ScheduleMode) getIntent().getSerializableExtra(ARG_MODE);
        id = getIntent().getIntExtra(ARG_ID, DEFAULT_ID);
        //name = getIntent().getStringExtra(ARG_NAME);
        //if (name == null) { name = "null"; }

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

    private void initData() {
        List<ScheduleItem> list = new ArrayList<>();

        ScheduleItemHeader header = new ScheduleItemHeader();
        header.setTitle(String.format("Понедельник, 28 января"));
        list.add(header);

        ScheduleItem item = new ScheduleItem();
        item.setStart("10:00");
        item.setEnd("11:00");
        item.setType("ПРАКТИЧЕСКОЕ ЗАНЯТИЕ");
        item.setName("Анализ данных (анг)");
        item.setPlace("Ауд. 503, Кочновский пр-д, д.3");
        item.setTeacher("Пред. Гущим Михаил Иванович");
        list.add(item);

        item = new ScheduleItem();
        item.setStart("12:00");
        item.setEnd("13:00");
        item.setType("ПРАКТИЧЕСКОЕ ЗАНЯТИЕ");
        item.setName("Анализ данных (анг)");
        item.setPlace("Ауд. 503, Кочновский пр-д, д.3");
        item.setTeacher("Пред. Гущим Михаил Иванович");
        list.add(item);
        adapter.setDataList(list);
    }

    public final static class ItemAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {

        private final static int TYPE_ITEM = 0;
        private final static int TYPE_HEADER = 1;

        private List<ScheduleItem> dataList = new ArrayList<>();
        private OnItemClick onItemClick;

        public ItemAdapter(OnItemClick onItemClick) { this.onItemClick = onItemClick; }

        @NonNull
        @Override
        public RecyclerView.ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
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
        public void onBindViewHolder(@NonNull RecyclerView.ViewHolder viewHolder, int position) {
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