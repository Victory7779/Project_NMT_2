using Project_NMT_2.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Linq;

namespace Project_NMT_2
{
    public partial class TestNMT : DbContext
    {
        public DbSet<UserPersonalInfomation> UserPersonalInfomations { get; set; }
        public DbSet<InitializationUser> InitializationUsers { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<SubjectUsers> SubjectUsers { get; set; }
        public DbSet<RatingUsers> RatingUsers { get; set; }
        public DbSet<UkrainianSchoolPerformance> UkrainianSchoolPerformances { get; set;}
        public DbSet<MathematicsSchoolPerformance> MathematicsSchoolPerformances { get; set;}
        public DbSet<HistorySchoolPerformance> HistorySchoolPerformances { get; set; }
        public DbSet<InitializationAdmin> InitializationAdmins { get; set; }
        public DbSet<SchoolSubjects> SchoolSubjects { get; set; }
        public DbSet<ALLTest> ALLTests { get; set; }
        public DbSet<PassedTest> PassedTests { get; set; }
        public DbSet<QuestionsForTest> QuestionsForTests { get; set; }
        public DbSet<SingleChoiceAnswer> SingleChoiceAnswers { get; set;}
        public DbSet<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }
        public DbSet<MachingAnswer> MachingAnswers { get; set; }
        public DbSet<OpenAnswer> OpenAnswers { get; set; }
        public TestNMT()
            : base("name=TestNMT")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InitializationUser>()
                .HasRequired(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.id_user);
               // .WillCascadeOnDelete();

            modelBuilder.Entity<Reviews>()
                .HasRequired(r => r.User)
                .WithMany(u => u.Reviewss)
                .HasForeignKey(r => r.id_user)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SubjectUsers>()
                .HasRequired(s => s.User)
                .WithMany()
                .HasForeignKey(s=>s.id_user)
                .WillCascadeOnDelete();

            modelBuilder.Entity<RatingUsers>()
                .HasRequired(r => r.SubjectUsers)
                .WithMany()
                .HasForeignKey(r => r.id_subject)
                .WillCascadeOnDelete();

            modelBuilder.Entity<UkrainianSchoolPerformance>()
                .HasRequired(u => u.SubjectUsers)
                .WithMany(s => s.UkrainianSchoolPerformances)
                .HasForeignKey(u => u.id_subject)
                .WillCascadeOnDelete();

            modelBuilder.Entity<MathematicsSchoolPerformance>()
                .HasRequired(m => m.SubjectUsers)
                .WithMany(s => s.MathematicsSchoolPerformances)
                .HasForeignKey(m => m.id_subject)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HistorySchoolPerformance>()
                .HasRequired(h => h.SubjectUsers)
                .WithMany(s => s.HistorySchoolPerformances)
                .HasForeignKey(h => h.id_subject)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ALLTest>()
                .HasRequired(a => a.SchoolSubjects)
                .WithMany(s => s.ALLTests)
                .HasForeignKey(a => a.id_subject);

            modelBuilder.Entity<PassedTest>()
                .HasRequired(p => p.User)
                .WithMany(u => u.Tests)
                .HasForeignKey(p => p.id_user)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PassedTest>()
                .HasRequired(p => p.ALLTests)
                .WithMany(a => a.PassedTests)
                .HasForeignKey(p => p.id_test)
                .WillCascadeOnDelete();

            modelBuilder.Entity<QuestionsForTest>()
                .HasRequired(q => q.ALLTest)
                .WithMany(a => a.QuestionsForTests)
                .HasForeignKey(q => q.id_test);

            modelBuilder.Entity<SingleChoiceAnswer>()
                .HasRequired(s => s.QuestionsForTest)
                .WithMany(q => q.SingleChoiceAnswers)
                .HasForeignKey(s => s.id_question)
                .WillCascadeOnDelete();

            modelBuilder.Entity<MultipleChoiceAnswer>()
                .HasRequired(m => m.QuestionsForTest)
                .WithMany(q => q.MultipleChoiceAnswers)
                .HasForeignKey(m => m.id_question)
                .WillCascadeOnDelete();

            modelBuilder.Entity<MachingAnswer>()
                .HasRequired(m => m.QuestionsForTest)
                .WithMany(q => q.MachingAnswers)
                .HasForeignKey(m => m.id_question)
                .WillCascadeOnDelete();

            modelBuilder.Entity<OpenAnswer>()
                .HasRequired(o => o.QuestionsForTest)
                .WithMany(q => q.OpenAnswers)
                .HasForeignKey(o => o.id_question)
                .WillCascadeOnDelete();


        }
    }
}
