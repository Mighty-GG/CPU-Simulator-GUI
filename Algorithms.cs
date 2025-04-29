using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static PerformanceMetrics fcfsAlgorithm(string userInput)
        {
            int np;
            if (!int.TryParse(userInput, out np) || np <= 0)
            {
                MessageBox.Show("Invalid number of processes. Please enter a valid positive integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int[] burstTime = new int[np];
            int[] waitingTime = new int[np];
            int[] turnaroundTime = new int[np];

            int totalBurstTime = 0;
            int currentTime = 0;

            //input burst times
            for (int i = 0; i < np; i++)
            {
                string burstInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:", "Burst Time", "", -1, -1);
                burstTime[i] = Convert.ToInt32(burstInput);
                totalBurstTime += burstTime[i];
            }

            //calculate waiting time
            waitingTime[0] = 0;
            for (int i = 1; i < np; i++)
            {
                waitingTime[i] = waitingTime[i - 1] + burstTime[i - 1];
            }

            //calculate turnaround time
            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = waitingTime[i] + burstTime[i];
            }

            double totalWT = 0, totalTAT = 0;
            for (int i = 0; i < np; i++)
            {
                totalWT += waitingTime[i];
                totalTAT += turnaroundTime[i];
            }

            currentTime = waitingTime[np - 1] + burstTime[np - 1]; //total elapsed time = finish of last process

            double cpuUtilization = (totalBurstTime * 1.0 / currentTime) * 100;
            double throughput = np * 1.0 / currentTime;

            return new PerformanceMetrics
            {
                AlgorithmName = "FCFS",
                AverageWaitingTime = totalWT / np,
                AverageTurnaroundTime = totalTAT / np,
                CPUUtilization = cpuUtilization,
                Throughput = throughput
            };

        }

        public static PerformanceMetrics sjfAlgorithm(string userInput)
        {
            int np;
            if (!int.TryParse(userInput, out np) || np <= 0)
            {
                MessageBox.Show("Invalid number of processes. Please enter a valid positive integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int[] burstTime = new int[np];
            int[] waitingTime = new int[np];
            int[] turnaroundTime = new int[np];
            int[] sortedBurstTime = new int[np];

            int totalBurstTime = 0;
            int currentTime = 0;

            //input burst times
            for (int i = 0; i < np; i++)
            {
                string burstInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:", "Burst Time", "", -1, -1);
                burstTime[i] = Convert.ToInt32(burstInput);
                sortedBurstTime[i] = burstTime[i];
                totalBurstTime += burstTime[i];
            }

            //sort burst times
            Array.Sort(sortedBurstTime);

            //calculate waiting time
            waitingTime[0] = 0;
            for (int i = 1; i < np; i++)
            {
                waitingTime[i] = waitingTime[i - 1] + sortedBurstTime[i - 1];
            }

            //calculate turnaround time
            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = waitingTime[i] + sortedBurstTime[i];
            }

            double totalWT = 0, totalTAT = 0;
            for (int i = 0; i < np; i++)
            {
                totalWT += waitingTime[i];
                totalTAT += turnaroundTime[i];
            }

            currentTime = waitingTime[np - 1] + sortedBurstTime[np - 1]; //total elapsed time

            double cpuUtilization = (totalBurstTime * 1.0 / currentTime) * 100;
            double throughput = np * 1.0 / currentTime;

            return new PerformanceMetrics
            {
                AlgorithmName = "SJF",
                AverageWaitingTime = totalWT / np,
                AverageTurnaroundTime = totalTAT / np,
                CPUUtilization = cpuUtilization,
                Throughput = throughput
            };
        }

        public static PerformanceMetrics priorityAlgorithm(string userInput)
        {
            int np;
            if (!int.TryParse(userInput, out np) || np <= 0)
            {
                MessageBox.Show("Invalid number of processes. Please enter a valid positive integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int[] burstTime = new int[np];
            int[] priority = new int[np];
            int[] waitingTime = new int[np];
            int[] turnaroundTime = new int[np];

            int totalBurstTime = 0;
            int currentTime = 0;

            //input burst times and priorities
            for (int i = 0; i < np; i++)
            {
                string burstInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:", "Burst Time", "", -1, -1);
                string priorityInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter priority for P{i + 1} (lower number = higher priority):", "Priority", "", -1, -1);

                burstTime[i] = Convert.ToInt32(burstInput);
                priority[i] = Convert.ToInt32(priorityInput);
                totalBurstTime += burstTime[i];
            }

            //sort by priority
            int[] sortedIndices = Enumerable.Range(0, np)
                                            .OrderBy(i => priority[i])
                                            .ToArray();

            waitingTime[sortedIndices[0]] = 0;
            for (int i = 1; i < np; i++)
            {
                waitingTime[sortedIndices[i]] = waitingTime[sortedIndices[i - 1]] + burstTime[sortedIndices[i - 1]];
            }

            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = waitingTime[i] + burstTime[i];
            }

            double totalWT = 0, totalTAT = 0;
            for (int i = 0; i < np; i++)
            {
                totalWT += waitingTime[i];
                totalTAT += turnaroundTime[i];
            }

            currentTime = waitingTime[sortedIndices[np - 1]] + burstTime[sortedIndices[np - 1]];

            double cpuUtilization = (totalBurstTime * 1.0 / currentTime) * 100;
            double throughput = np * 1.0 / currentTime;

            return new PerformanceMetrics
            {
                AlgorithmName = "Priority",
                AverageWaitingTime = totalWT / np,
                AverageTurnaroundTime = totalTAT / np,
                CPUUtilization = cpuUtilization,
                Throughput = throughput
            };

        }

        public static PerformanceMetrics roundRobinAlgorithm(string userInput)
        {
            int np;
            if (!int.TryParse(userInput, out np) || np <= 0)
            {
                MessageBox.Show("Invalid number of processes. Please enter a valid positive integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int[] arrivalTime = new int[np];
            int[] burstTime = new int[np];
            int[] remainingTime = new int[np];
            int[] waitingTime = new int[np];
            int[] turnaroundTime = new int[np];
            int totalBurstTime = 0;

            int currentTime = 0;
            int completed = 0;

            //input arrival and burst times
            for (int i = 0; i < np; i++)
            {
                string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter arrival time for P{i + 1}:", "Arrival Time", "", -1, -1);
                string burstInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:", "Burst Time", "", -1, -1);

                arrivalTime[i] = Convert.ToInt32(arrivalInput);
                burstTime[i] = Convert.ToInt32(burstInput);
                remainingTime[i] = burstTime[i];
                totalBurstTime += burstTime[i];
            }

            string quantumInput = Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum", "", -1, -1);
            int quantum = Convert.ToInt32(quantumInput);

            Queue<int> processQueue = new Queue<int>();
            bool[] addedToQueue = new bool[np];

            while (completed < np)
            {
                for (int i = 0; i < np; i++)
                {
                    if (arrivalTime[i] <= currentTime && remainingTime[i] > 0 && !addedToQueue[i])
                    {
                        processQueue.Enqueue(i);
                        addedToQueue[i] = true;
                    }
                }

                if (processQueue.Count == 0)
                {
                    currentTime++;
                    continue;
                }

                int idx = processQueue.Dequeue();
                int execTime = Math.Min(quantum, remainingTime[idx]);
                currentTime += execTime;
                remainingTime[idx] -= execTime;

                //add newly arrived processes during execution
                for (int i = 0; i < np; i++)
                {
                    if (arrivalTime[i] <= currentTime && remainingTime[i] > 0 && !addedToQueue[i])
                    {
                        processQueue.Enqueue(i);
                        addedToQueue[i] = true;
                    }
                }

                if (remainingTime[idx] > 0)
                {
                    processQueue.Enqueue(idx); //re-queue if not finished
                }
                else
                {
                    completed++;
                    turnaroundTime[idx] = currentTime - arrivalTime[idx];
                    waitingTime[idx] = turnaroundTime[idx] - burstTime[idx];
                }
            }

            double totalWT = 0, totalTAT = 0;
            for (int i = 0; i < np; i++)
            {
                totalWT += waitingTime[i];
                totalTAT += turnaroundTime[i];
            }

            double cpuUtilization = (totalBurstTime * 1.0 / currentTime) * 100;
            double throughput = np * 1.0 / currentTime;

            return new PerformanceMetrics
            {
                AlgorithmName = "Round Robin",
                AverageWaitingTime = totalWT / np,
                AverageTurnaroundTime = totalTAT / np,
                CPUUtilization = cpuUtilization,
                Throughput = throughput
            };

        }
        //------------------------------------------------------

        public static PerformanceMetrics srtfAlgorithm(string userInput)
        {
            int np;
            if (!int.TryParse(userInput, out np) || np <= 0)
            {
                MessageBox.Show("Invalid number of processes.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int[] arrivalTime = new int[np];
            int[] burstTime = new int[np];
            int[] remainingTime = new int[np];
            int[] waitingTime = new int[np];
            int[] turnaroundTime = new int[np];
            int[] startTime = new int[np];

            int completed = 0, currentTime = 0, totalBurstTime = 0;
            int minRemaining = int.MaxValue;
            int shortest = 0;
            bool check = false;

            //inputs
            for (int i = 0; i < np; i++)
            {
                string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter arrival time for P{i + 1}:", "Arrival Time", "", -1, -1);
                string burstInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:", "Burst Time", "", -1, -1);

                arrivalTime[i] = Convert.ToInt32(arrivalInput);
                burstTime[i] = Convert.ToInt32(burstInput);
                remainingTime[i] = burstTime[i];
                totalBurstTime += burstTime[i];
            }

            while (completed != np)
            {
                for (int j = 0; j < np; j++)
                {
                    if ((arrivalTime[j] <= currentTime) && (remainingTime[j] < minRemaining) && remainingTime[j] > 0)
                    {
                        minRemaining = remainingTime[j];
                        shortest = j;
                        check = true;
                    }
                }

                if (!check)
                {
                    currentTime++;
                    continue;
                }

                remainingTime[shortest]--;

                minRemaining = remainingTime[shortest];
                if (minRemaining == 0)
                    minRemaining = int.MaxValue;

                if (remainingTime[shortest] == 0)
                {
                    completed++;
                    int finishTime = currentTime + 1;
                    waitingTime[shortest] = finishTime - burstTime[shortest] - arrivalTime[shortest];

                    if (waitingTime[shortest] < 0)
                        waitingTime[shortest] = 0;
                }
                currentTime++;
            }

            double totalWT = 0, totalTAT = 0;
            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = burstTime[i] + waitingTime[i];
                totalWT += waitingTime[i];
                totalTAT += turnaroundTime[i];
            }

            //calculate CPU utilization and throughput
            double cpuUtilization = (totalBurstTime * 1.0 / currentTime) * 100;
            double throughput = np * 1.0 / currentTime;

            return new PerformanceMetrics
            {
                AlgorithmName = "SRTF",
                AverageWaitingTime = totalWT / np,
                AverageTurnaroundTime = totalTAT / np,
                CPUUtilization = cpuUtilization,
                Throughput = throughput
            };

        }

        public static PerformanceMetrics mlfqAlgorithm(string userInput)
        {
            int np;
            if (!int.TryParse(userInput, out np) || np <= 0)
            {
                MessageBox.Show("Invalid number of processes. Please enter a valid positive integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int[] arrivalTime = new int[np];
            int[] burstTime = new int[np];
            int[] remainingTime = new int[np];
            int[] completionTime = new int[np];
            int[] waitingTime = new int[np];
            int[] turnaroundTime = new int[np];

            int currentTime = 0, completed = 0, totalBurstTime = 0;

            int quantum1 = 4;
            int quantum2 = 8;

            //input
            for (int i = 0; i < np; i++)
            {
                string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter arrival time for P{i + 1}:", "Arrival Time", "", -1, -1);
                string burstInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:", "Burst Time", "", -1, -1);

                arrivalTime[i] = Convert.ToInt32(arrivalInput);
                burstTime[i] = Convert.ToInt32(burstInput);
                remainingTime[i] = burstTime[i];
                totalBurstTime += burstTime[i];
            }

            Queue<int> queue1 = new Queue<int>();
            Queue<int> queue2 = new Queue<int>();
            Queue<int> queue3 = new Queue<int>();
            bool[] addedToQueue = new bool[np];

            while (completed < np)
            {
                for (int i = 0; i < np; i++)
                {
                    if (arrivalTime[i] <= currentTime && remainingTime[i] > 0 && !addedToQueue[i])
                    {
                        queue1.Enqueue(i);
                        addedToQueue[i] = true;
                    }
                }

                if (queue1.Count > 0)
                {
                    int idx = queue1.Dequeue();
                    int execTime = Math.Min(quantum1, remainingTime[idx]);
                    currentTime += execTime;
                    remainingTime[idx] -= execTime;

                    if (remainingTime[idx] > 0)
                    {
                        queue2.Enqueue(idx);
                    }
                    else
                    {
                        completionTime[idx] = currentTime;
                        completed++;
                    }
                }
                else if (queue2.Count > 0)
                {
                    int idx = queue2.Dequeue();
                    int execTime = Math.Min(quantum2, remainingTime[idx]);
                    currentTime += execTime;
                    remainingTime[idx] -= execTime;

                    if (remainingTime[idx] > 0)
                    {
                        queue3.Enqueue(idx);
                    }
                    else
                    {
                        completionTime[idx] = currentTime;
                        completed++;
                    }
                }
                else if (queue3.Count > 0)
                {
                    int idx = queue3.Dequeue();
                    currentTime += remainingTime[idx];
                    remainingTime[idx] = 0;
                    completionTime[idx] = currentTime;
                    completed++;
                }
                else
                {
                    currentTime++;
                }
            }

            double totalWT = 0, totalTAT = 0;
            for (int i = 0; i < np; i++)
            {
                turnaroundTime[i] = completionTime[i] - arrivalTime[i];
                waitingTime[i] = turnaroundTime[i] - burstTime[i];

                totalWT += waitingTime[i];
                totalTAT += turnaroundTime[i];
            }

            double cpuUtilization = (totalBurstTime * 1.0 / currentTime) * 100;
            double throughput = np * 1.0 / currentTime;

            return new PerformanceMetrics
            {
                AlgorithmName = "MLFQ",
                AverageWaitingTime = totalWT / np,
                AverageTurnaroundTime = totalTAT / np,
                CPUUtilization = cpuUtilization,
                Throughput = throughput
            };

        }


    }

    public class PerformanceMetrics
    {
        public string AlgorithmName { get; set; }
        public double AverageWaitingTime { get; set; }
        public double AverageTurnaroundTime { get; set; }
        public double CPUUtilization { get; set; }
        public double Throughput { get; set; }
    }

}

