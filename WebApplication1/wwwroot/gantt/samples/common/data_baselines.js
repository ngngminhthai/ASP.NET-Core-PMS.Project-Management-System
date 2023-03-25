var taskData = {
  "data": [
    {
      "id": "1",
      "start_date": "2018-04-01 00:00:00",
      "duration": "5",
      "text": "Project #1",
      "progress": "0.8",
      "parent": "0",
      "deadline": "2018-04-09 00:00:00",
      "planned_start": "2018-04-01 00:00:00",
      "planned_end": "2018-04-07 00:00:00",
      "open": 1
    },
    {
      "id": "2",
      "start_date": "2018-04-06 00:00:00",
      "duration": "4",
      "text": "Task #1",
      "progress": "0.5",
      "parent": "1",
      "deadline": "2018-04-11 00:00:00",
      "planned_start": "2018-04-06 00:00:00",
      "planned_end": "2018-04-10 00:00:00",
      "open": 1
    },
    {
      "id": "3",
      "start_date": "2018-04-03 00:00:00",
      "duration": "6",
      "text": "Task #2",
      "progress": "0.7",
      "parent": "1",
      "deadline": "2018-04-10 00:00:00",
      "planned_start": "2018-04-05 00:00:00",
      "planned_end": "2018-04-14 00:00:00",
      "open": 1
    },
    {
      "id": "4",
      "start_date": "2018-04-07 00:00:00",
      "duration": "2",
      "text": "Task #3",
      "progress": "0",
      "parent": "1",
      "deadline": "2018-04-17 00:00:00",
      "planned_start": "2018-04-03 00:00:00",
      "planned_end": "2018-04-05 00:00:00",
      "open": 1
    },
    {
      "id": "5",
      "start_date": "2018-04-05 00:00:00",
      "duration": "5",
      "text": "Task #1.1",
      "progress": "0.34",
      "parent": "2",
      "deadline": "2018-04-10 00:00:00",
      "planned_start": "2018-04-03 00:00:00",
      "planned_end": "2018-04-08 00:00:00",
      "open": 1
    },
    {
      "id": "6",
      "start_date": "2018-04-11 00:00:00",
      "duration": "4",
      "text": "Task #1.2",
      "progress": "0.491477",
      "parent": "2",
      "deadline": "2018-04-15 00:00:00",
      "planned_start": "2018-04-11 00:00:00",
      "planned_end": "2018-04-16 00:00:00",
      "open": 1
    },
    {
      "id": "7",
      "start_date": "2018-04-07 00:00:00",
      "duration": "5",
      "text": "Task #2.1",
      "progress": "0.2",
      "parent": "3",
      "deadline": "2018-04-11 00:00:00",
      "planned_start": "2018-04-07 00:00:00",
      "planned_end": "2018-04-12 00:00:00",
      "open": 1
    },
    {
      "id": "8",
      "start_date": "2018-04-06 00:00:00",
      "duration": "4",
      "text": "Task #2.2",
      "progress": "0.9",
      "parent": "3",
      "deadline": "2018-04-16 00:00:00",
      "planned_start": "2018-04-06 00:00:00",
      "planned_end": "2018-04-10 00:00:00",
      "open": 1
    },
    {
      "id": "9",
      "start_date": "2018-04-06 00:00:00",
      "duration": "5",
      "text": "Task #3.1",
      "progress": "1",
      "parent": "4",
      "deadline": "2018-04-16 00:00:00",
      "planned_start": "2018-04-06 00:00:00",
      "planned_end": "2018-04-11 00:00:00",
      "open": 1
    },
    {
      "id": "10",
      "start_date": "2018-04-06 00:00:00",
      "duration": "3",
      "text": "Task #3.2",
      "progress": "0",
      "parent": "4",
      "deadline": "2018-04-11 00:00:00",
      "planned_start": "2018-04-05 00:00:00",
      "planned_end": "2018-04-08 00:00:00",
      "open": 1
    },
    {
      "id": "11",
      "start_date": "2018-04-06 00:00:00",
      "duration": "4",
      "text": "Task #3.3",
      "progress": "0.33",
      "parent": "4",
      "deadline": "2018-04-10 00:00:00",
      "planned_start": "2018-04-07 00:00:00",
      "planned_end": "2018-04-11 00:00:00",
      "open": 1
    },
    {
      "id": "12",
      "start_date": "2018-04-02 00:00:00",
      "duration": "8",
      "text": "Project #2",
      "progress": "0",
      "parent": "0",
      "deadline": "2018-04-04 00:00:00",
      "planned_start": "2018-04-02 00:00:00",
      "planned_end": "2018-04-20 00:00:00",
      "open": 1
    },
    {
      "id": "13",
      "start_date": "2018-04-02 00:00:00",
      "duration": "10",
      "text": "Task #1",
      "progress": "0.2",
      "parent": "12",
      "deadline": "2018-04-09 00:00:00",
      "planned_start": "2018-04-02 00:00:00",
      "planned_end": "2018-04-12 00:00:00",
      "open": 1
    },
    {
      "id": "14",
      "start_date": "2018-04-04 00:00:00",
      "duration": "4",
      "text": "Task #2",
      "progress": "0.9",
      "parent": "12",
      "deadline": "2018-04-09 00:00:00",
      "planned_start": "2018-04-04 00:00:00",
      "planned_end": "2018-04-08 00:00:00",
      "open": 1
    },
    {
      "id": "15",
      "start_date": "2018-04-05 00:00:00",
      "duration": "3",
      "text": "Task #3",
      "progress": "0.6",
      "parent": "12",
      "deadline": "2018-04-09 00:00:00",
      "planned_start": "2018-04-05 00:00:00",
      "planned_end": "2018-04-08 00:00:00",
      "open": 1
    },
    {
      "id": "16",
      "start_date": "2018-04-01 00:00:00",
      "duration": "3",
      "text": "Task #4",
      "progress": "0.214286",
      "parent": "12",
      "deadline": "2018-04-05 00:00:00",
      "planned_start": "2018-04-01 00:00:00",
      "planned_end": "2018-04-04 00:00:00",
      "open": 1
    },
    {
      "id": "17",
      "start_date": "2018-04-06 00:00:00",
      "duration": "6",
      "text": "Task #5",
      "progress": "0.5",
      "parent": "12",
      "deadline": "2018-04-12 00:00:00",
      "planned_start": "2018-04-06 00:00:00",
      "planned_end": "2018-04-12 00:00:00",
      "open": 1
    },
    {
      "id": "18",
      "start_date": "2018-04-05 00:00:00",
      "duration": "5",
      "text": "Task #2.1",
      "progress": "0.3",
      "parent": "14",
      "deadline": "2018-04-09 00:00:00",
      "planned_start": "2018-04-07 00:00:00",
      "planned_end": "2018-04-12 00:00:00",
      "open": 1
    },
    {
      "id": "19",
      "start_date": "2018-04-05 00:00:00",
      "duration": "6",
      "text": "Task #2.2",
      "progress": "0.453052",
      "parent": "14",
      "deadline": "2018-04-09 00:00:00",
      "planned_start": "2018-04-08 00:00:00",
      "planned_end": "2018-04-14 00:00:00",
      "open": 1
    },
    {
      "id": "20",
      "start_date": "2018-04-05 00:00:00",
      "duration": "4",
      "text": "Task #2.3",
      "progress": "0.512605",
      "parent": "14",
      "deadline": "2018-04-08 00:00:00",
      "planned_start": "2018-04-03 00:00:00",
      "planned_end": "2018-04-07 00:00:00",
      "open": 1
    },
    {
      "id": "21",
      "start_date": "2018-04-05 00:00:00",
      "duration": "6",
      "text": "Task #2.4",
      "progress": "0.7",
      "parent": "14",
      "deadline": "2018-04-14 00:00:00",
      "planned_start": "2018-04-07 00:00:00",
      "planned_end": "2018-04-13 00:00:00",
      "open": 1
    },
    {
      "id": "22",
      "start_date": "2018-04-05 00:00:00",
      "duration": "7",
      "text": "Task #4.1",
      "progress": "1",
      "parent": "16",
      "deadline": "2018-04-15 00:00:00",
      "planned_start": "2018-04-05 00:00:00",
      "planned_end": "2018-04-12 00:00:00",
      "open": 1
    },
    {
      "id": "23",
      "start_date": "2018-04-05 00:00:00",
      "duration": "5",
      "text": "Task #4.2",
      "progress": "1",
      "parent": "16",
      "deadline": "2018-04-11 00:00:00",
      "planned_start": "2018-04-05 00:00:00",
      "planned_end": "2018-04-10 00:00:00",
      "open": 1
    },
    {
      "id": "24",
      "start_date": "2018-04-05 00:00:00",
      "duration": "5",
      "text": "Task #4.3",
      "progress": "0",
      "parent": "16",
      "deadline": "2018-04-12 00:00:00",
      "planned_start": "2018-04-05 00:00:00",
      "planned_end": "2018-04-10 00:00:00",
      "open": 1
    }
  ],
  "collections": {
    "links": [
      {
        "id": "1",
        "source": "1",
        "target": "2",
        "type": "0"
      },
      {
        "id": "2",
        "source": "1",
        "target": "3",
        "type": "0"
      },
      {
        "id": "3",
        "source": "1",
        "target": "4",
        "type": "0"
      },
      {
        "id": "4",
        "source": "2",
        "target": "6",
        "type": "0"
      }
    ]
  }
}