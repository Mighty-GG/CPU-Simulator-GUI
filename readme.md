## Author
- Josh Smith
- Course: CS 3502 - Operating Systems Section 03

## CPU-Simulator GUI
A project that simulate and evaluates multiple CPU scheduling algorithms. The simulator allows users to input and process data and compare the performance of different schedulaing strategies.

Originally, the starter project included four algorithms:
- First Come First Serve (FCFS)
- Shortest Job First (SJF)
- Round Robin (RR)
- Priority Scheduling

This branch extends the project by adding two advanced algorithms:
- Shortest Remaining Time First (SRTF)
- Multi-Level Feedback Queue (MLFQ)

A PerformanceMetrics class was also introduced to collect key results such as average waiting time, turnaround time, CPU utilization, and throughput.

## How to Build
1. Open the solution file: 'CpuSchedulingWinForms.sln' in Visual Studio.
2. Ensure all necessary '.cs' files are included along with its references.

## How to Run
1. Once built, run the project and enter the numer of processes you want to simulate.
2. Select one of the given CPU scheduling algorithms.
3. Provide the required arrival times, burst times, priorities (priority scheduling), and quantum time (round robin).
4. View performance metrics through pop-up summaries
5. After running multiple algorithms, use the 'Compare All' button to view a side-by-side performance comparisons.

## Features

- Implementation of six scheduling algorithms.
- Live entry of process parameters through the application.
- Collection and display of:
  - Average Waiting Time (AWT)
  - Average Turnaround Time (ATT)
  - CPU Utilization (%)
  - Throughput (Processes per Second)
- Comparative analysis feature.

## Modifications from Starter Code

- Implemented Shortest Remaining Time First (SRTF) algorithm.
- Implemented Multi-Level Feedback Queue (MLFQ) algorithm.
- Added a new 'PerformanceMetrics' structure to track algorithm performance.
- Extended GUI to include additional buttons and performance summaries.

## License
This project is licensed under the terms of the [MIT license](https://choosealicense.com/licenses/mit/).

The starter project can be found at: [https://github.com/FrancisNweke/CPU-Simulator-GUI]

