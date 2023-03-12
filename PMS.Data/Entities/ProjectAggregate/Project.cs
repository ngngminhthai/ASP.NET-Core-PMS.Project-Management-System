using PMS.Data.Entities;
using PMS.Data.Entities.ProjectAggregate;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApplication1.Data.Entities.ProjectAggregate
{
    public class Project : DomainEntity<int>, IDateTimeStamp
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ManageUser Creator { get; set; }
        public int? TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag? Tag { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<ProjectUploadedFile> ProjectUploadedFiles { get; set; }
        /*        public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
        */
        public List<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ProjectComment> ProjectComments { get; set; }
        public ICollection<ProjectRole> ProjectRoles { get; set; }


        public static List<ProjectTask> GetCriticalPath(ICollection<ProjectTask> tasks)
        {
            return tasks.Where(t => t.EarliestStart == t.LatestStart
                                    && t.EarliestFinish == t.LatestFinish)
                       .ToList();
        }

        public void CalculateSuccessorTasks()
        {
            foreach (var task in ProjectTasks)
            {
                foreach (var dependentTask in task.DependentTasks)
                {
                    dependentTask.SuccessorTaks.Add(task);
                }
            }
        }
        public void CalculateCriticalPath()
        {
            CalculateSuccessorTasks();

            List<ProjectTask> pts = OrderTask(ProjectTasks);
            /*  foreach (ProjectTask task in pts)
              {
                  Console.WriteLine($"Task {task.Id}: [{task.EarliestStart}|{task.EarliestFinish}|{task.LatestStart}|{task.LatestFinish}]");
              }
              Console.WriteLine("----------------------------");*/

            // Calculate earliest start and finish times
            foreach (ProjectTask task in ProjectTasks)
            {
                int maxDependentFinish = 0;
                foreach (ProjectTask dependentTask in task.DependentTasks)
                {
                    maxDependentFinish = Math.Max(maxDependentFinish, dependentTask.EarliestFinish);
                }
                task.EarliestStart = maxDependentFinish;
                task.EarliestFinish = task.EarliestStart + task.Duration;
            }

            /*   foreach (ProjectTask task in ProjectTasks)
               {
                   Console.WriteLine($"Task {task.Id}: [{task.EarliestStart}|{task.EarliestFinish}|{task.LatestStart}|{task.LatestFinish}]");
               }
               Console.WriteLine("----------------------------");*/


            // Calculate latest start and finish times
            foreach (ProjectTask task in ProjectTasks.Reverse<ProjectTask>())
            {
                int minSuccessorStart = ProjectTasks.Max(t => t.EarliestFinish);
                foreach (ProjectTask successorTask in task.SuccessorTaks)
                {
                    minSuccessorStart = Math.Min(minSuccessorStart, successorTask.LatestStart);
                }
                task.LatestFinish = minSuccessorStart;
                task.LatestStart = task.LatestFinish - task.Duration;
            }

            /*    foreach (ProjectTask task in ProjectTasks)
                {
                    Console.WriteLine($"Task {task.Id}: [{task.EarliestStart}|{task.EarliestFinish}|{task.LatestStart}|{task.LatestFinish}]");
                }
                Console.WriteLine("----------------------------");*/


            /*  foreach (ProjectTask task in ScheduleTasks(ProjectTasks))
              {
                  Console.WriteLine($"Task {task.Id}: [{task.Duration}|{task.EarliestStart}|{task.EarliestFinish}|{task.LatestStart}|{task.LatestFinish}]");
              }
              Console.WriteLine("----------------------------");*/
            DrawGanttChart();

            foreach (ProjectTask task in GetCriticalPath(ProjectTasks))
            {
                Console.WriteLine($"Task {task.Name}: [{task.Duration}|{task.EarliestStart}|{task.EarliestFinish}|{task.LatestStart}|{task.LatestFinish}]");
            }
            Console.WriteLine("----------------------------");


        }

        public List<ProjectTask> ScheduleTasks(List<ProjectTask> tasks)
        {
            List<ProjectTask> scheduledTasks = new List<ProjectTask>();
            var taskLookup = tasks.ToDictionary(t => t.Id, t => t);
            var sortedTasks = tasks.OrderBy(t => t.EarliestFinish);

            foreach (var task in sortedTasks)
            {
                scheduledTasks.Add(task);

                foreach (var successor in task.SuccessorTaks)
                {
                    ProjectTask successorTask = taskLookup[successor.Id];
                    int newEarliestStart = task.EarliestFinish;

                    if (successorTask.EarliestStart < newEarliestStart)
                    {
                        successorTask.EarliestStart = newEarliestStart;
                        successorTask.EarliestFinish = newEarliestStart + successorTask.Duration;
                        taskLookup[successor.Id] = successorTask;
                    }
                }
            }

            return scheduledTasks;
        }


        public List<ProjectTask> OrderTask(ICollection<ProjectTask> tasks)
        {
            List<ProjectTask> sortedTasks = new List<ProjectTask>();
            Dictionary<string, int> indegrees = new Dictionary<string, int>();
            Queue<ProjectTask> queue = new Queue<ProjectTask>();

            foreach (var task in tasks)
            {
                indegrees[task.Id.ToString()] = 0;
            }

            foreach (var task in tasks)
            {
                foreach (var successor in task.SuccessorTaks)
                {
                    indegrees[successor.Id.ToString()]++;
                }
            }

            foreach (var task in tasks)
            {
                if (indegrees[task.Id.ToString()] == 0)
                {
                    queue.Enqueue(task);
                }
            }

            while (queue.Count > 0)
            {
                var task = queue.Dequeue();
                sortedTasks.Add(task);

                foreach (var successor in task.SuccessorTaks)
                {
                    indegrees[successor.Id.ToString()]--;

                    if (indegrees[successor.Id.ToString()] == 0)
                    {
                        queue.Enqueue(successor);
                    }
                }
            }

            this.ProjectTasks = sortedTasks;
            return sortedTasks;
        }


        public void DrawGanttChart()
        {
            Console.WriteLine("Gantt Chart:");
            Console.WriteLine("-------------");

            int totalDuration = 0;
            foreach (var task in ProjectTasks)
            {
                totalDuration += task.Duration;
            }

            int[] startTimes = new int[ProjectTasks.Count];
            int[] endTimes = new int[ProjectTasks.Count];

            for (int i = 0; i < ProjectTasks.Count; i++)
            {
                ProjectTask task = ProjectTasks[i];

                int maxEndTime = 0;
                foreach (var dependentTask in task.DependentTasks)
                {
                    int dependentTaskIndex = ProjectTasks.IndexOf(dependentTask);
                    maxEndTime = Math.Max(maxEndTime, endTimes[dependentTaskIndex]);
                }

                startTimes[i] = maxEndTime;
                endTimes[i] = maxEndTime + task.Duration;

                Console.WriteLine("Task " + task.Id + " starts at " + startTimes[i] + " and ends at " + endTimes[i]);
            }
        }

        public ICollection<KanbanColume> kanbanColumes { get; set; }
    }
}
