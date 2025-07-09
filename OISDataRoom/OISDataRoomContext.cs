using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OISPublic.OISDataRoom;

public partial class OISDataRoomContext : DbContext
{

    private readonly IConfiguration _configuration;

    public OISDataRoomContext(DbContextOptions<OISDataRoomContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }

    public virtual DbSet<AnnouncementSetting> AnnouncementSettings { get; set; }

    public virtual DbSet<AwsBucket> AwsBuckets { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryCompanyLocation> CategoryCompanyLocations { get; set; }

    public virtual DbSet<CategoryName> CategoryNames { get; set; }

    public virtual DbSet<CloudDetail> CloudDetails { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<DailyReminder> DailyReminders { get; set; }

    public virtual DbSet<DataRoom> DataRooms { get; set; }

    public virtual DbSet<DataRoomAuditTrial> DataRoomAuditTrials { get; set; }

    public virtual DbSet<DataRoomFile> DataRoomFiles { get; set; }

    public virtual DbSet<DataRoomMasterUser> DataRoomMasterUsers { get; set; }

    public virtual DbSet<DataRoomUser> DataRoomUsers { get; set; }

    public virtual DbSet<DeleteArchivePurgeSetting> DeleteArchivePurgeSettings { get; set; }

    public virtual DbSet<DesktopSpaceDetail> DesktopSpaceDetails { get; set; }

    public virtual DbSet<DocCombine> DocCombines { get; set; }

    public virtual DbSet<DocCombineDocument> DocCombineDocuments { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentAuditTrail> DocumentAuditTrails { get; set; }

    public virtual DbSet<DocumentComment> DocumentComments { get; set; }

    public virtual DbSet<DocumentDeleteHistory> DocumentDeleteHistories { get; set; }

    public virtual DbSet<DocumentExternalLinkSharing> DocumentExternalLinkSharings { get; set; }

    public virtual DbSet<DocumentMetaData> DocumentMetaDatas { get; set; }

    public virtual DbSet<DocumentRolePermission> DocumentRolePermissions { get; set; }

    public virtual DbSet<DocumentShareLinkPermission> DocumentShareLinkPermissions { get; set; }

    public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }

    public virtual DbSet<DocumentToken> DocumentTokens { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<DocumentUserPermission> DocumentUserPermissions { get; set; }

    public virtual DbSet<DocumentVersion> DocumentVersions { get; set; }

    public virtual DbSet<DocumentsDetail> DocumentsDetails { get; set; }

    public virtual DbSet<EmailRule> EmailRules { get; set; }

    public virtual DbSet<EmailSmtpsetting> EmailSmtpsettings { get; set; }

    public virtual DbSet<EmailSyncSetting> EmailSyncSettings { get; set; }

    public virtual DbSet<EmailSyncTable> EmailSyncTables { get; set; }

    public virtual DbSet<FavouriteDocument> FavouriteDocuments { get; set; }

    public virtual DbSet<FileStorageSetting> FileStorageSettings { get; set; }

    public virtual DbSet<FolderAuditTrail> FolderAuditTrails { get; set; }

    public virtual DbSet<FolderIndexingConfiguration> FolderIndexingConfigurations { get; set; }

    public virtual DbSet<FolderIndexingScheme> FolderIndexingSchemes { get; set; }

    public virtual DbSet<FolderResetFrequency> FolderResetFrequencies { get; set; }

    public virtual DbSet<FolderSizingConfiguration> FolderSizingConfigurations { get; set; }

    public virtual DbSet<FolderTemplate> FolderTemplates { get; set; }

    public virtual DbSet<HalfYearlyReminder> HalfYearlyReminders { get; set; }

    public virtual DbSet<Hash> Hashes { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobParameter> JobParameters { get; set; }

    public virtual DbSet<JobQueue> JobQueues { get; set; }

    public virtual DbSet<LinkDocument> LinkDocuments { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<LoginAudit> LoginAudits { get; set; }

    public virtual DbSet<Nlog> Nlogs { get; set; }

    public virtual DbSet<NotificationAccessSetting> NotificationAccessSettings { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<PersonalFolderSizeSetting> PersonalFolderSizeSettings { get; set; }

    public virtual DbSet<QuarterlyReminder> QuarterlyReminders { get; set; }

    public virtual DbSet<Reminder> Reminders { get; set; }

    public virtual DbSet<ReminderNotification> ReminderNotifications { get; set; }

    public virtual DbSet<ReminderScheduler> ReminderSchedulers { get; set; }

    public virtual DbSet<ReminderType> ReminderTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleClaim> RoleClaims { get; set; }

    public virtual DbSet<RoleCompanyLocation> RoleCompanyLocations { get; set; }

    public virtual DbSet<RoleFolderPermission> RoleFolderPermissions { get; set; }

    public virtual DbSet<Schema> Schemas { get; set; }

    public virtual DbSet<Screen> Screens { get; set; }

    public virtual DbSet<ScreenOperation> ScreenOperations { get; set; }

    public virtual DbSet<SendEmail> SendEmails { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<ShareLinkPermission> ShareLinkPermissions { get; set; }

    public virtual DbSet<Smssetting> Smssettings { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<TCompanyMaster> TCompanyMasters { get; set; }

    public virtual DbSet<TLocation> TLocations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserClaim> UserClaims { get; set; }

    public virtual DbSet<UserFolder> UserFolders { get; set; }

    public virtual DbSet<UserFolderDragAndDrop> UserFolderDragAndDrops { get; set; }

    public virtual DbSet<UserFolderPermission> UserFolderPermissions { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserNotification> UserNotifications { get; set; }

    public virtual DbSet<UserNotificationAccessSetting> UserNotificationAccessSettings { get; set; }

    public virtual DbSet<UserQuery> UserQueries { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    public virtual DbSet<VersionScreen> VersionScreens { get; set; }

    public virtual DbSet<WhatsappLog> WhatsappLogs { get; set; }

    public virtual DbSet<WhatsappSetting> WhatsappSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("dmsdb_connectionstring"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AggregatedCounter>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PK_HangFire_CounterAggregated");

            entity.ToTable("AggregatedCounter", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<AnnouncementSetting>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AnnouncementSetting");
        });

        modelBuilder.Entity<AwsBucket>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AccessKey).HasMaxLength(255);
            entity.Property(e => e.BucketName).HasMaxLength(255);
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.Region).HasMaxLength(255);
            entity.Property(e => e.SecretKey).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(255);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.ParentId, "IX_Categories_ParentId");

            entity.HasIndex(e => new { e.Name, e.ParentId }, "idx_nameparent");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FolderPosition).IsUnicode(false);
            entity.Property(e => e.FolderSource)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Permission)
                .HasDefaultValue(false)
                .HasColumnName("permission");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasForeignKey(d => d.ParentId);
        });

        modelBuilder.Entity<CategoryCompanyLocation>(entity =>
        {
            entity.ToTable("CategoryCompanyLocation");

            entity.HasIndex(e => new { e.CategoryId, e.CompanyId }, "idx_CategoryComp");

            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.CategoryCompanyLocations)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_CategoryCompanyLocation_Categories");
        });

        modelBuilder.Entity<CategoryName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07166A037C");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Permission).HasColumnName("permission");
        });

        modelBuilder.Entity<CloudDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CloudDet__3214EC270AC9492D");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.AuthName).HasMaxLength(15);
            entity.Property(e => e.AuthToken).HasMaxLength(255);
            entity.Property(e => e.AuthType).HasMaxLength(50);
            entity.Property(e => e.AuthUrl).HasMaxLength(255);
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Counter", "HangFire");

            entity.HasIndex(e => e.Key, "CX_HangFire_Counter").IsClustered();

            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.Key).HasMaxLength(100);
        });

        modelBuilder.Entity<DailyReminder>(entity =>
        {
            entity.HasIndex(e => e.ReminderId, "IX_DailyReminders_ReminderId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Reminder).WithMany(p => p.DailyReminders).HasForeignKey(d => d.ReminderId);
        });

        modelBuilder.Entity<DataRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DataRoom__3214EC0734973679");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(100);
            entity.Property(e => e.DefaultPermission).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<DataRoomAuditTrial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DataRoom__3214EC070ADA7F24");

            entity.ToTable("DataRoomAuditTrial");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActionName).HasMaxLength(100);
            entity.Property(e => e.ClientId).HasMaxLength(100);
            entity.Property(e => e.DataRoomName).HasMaxLength(100);
            entity.Property(e => e.DocumentName).HasMaxLength(100);
        });

        modelBuilder.Entity<DataRoomFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DataRoom__3214EC07C388732F");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByName).HasMaxLength(100);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Permission)
                .HasMaxLength(20)
                .HasDefaultValue("inherit");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<DataRoomMasterUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DataRoom__3214EC071FCAB51F");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<DataRoomUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DataRoom__3214EC07C502DCAD");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccessLevel).HasMaxLength(100);
            entity.Property(e => e.ClientId).HasMaxLength(100);
            entity.Property(e => e.CreatedByName).HasMaxLength(100);
        });

        modelBuilder.Entity<DeleteArchivePurgeSetting>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DesktopSpaceDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DesktopS__3214EC0725684743");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.DesktopSpaceDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DesktopSpaceDetailsss_UserId");
        });

        modelBuilder.Entity<DocCombine>(entity =>
        {
            entity.HasKey(e => e.DocCombineId).HasName("PK__DocCombi__04012A4639F347D2");

            entity.ToTable("DocCombine");

            entity.Property(e => e.DocCombineId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ClientId).HasMaxLength(35);
            entity.Property(e => e.CloudSlug).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocType).HasMaxLength(100);
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Processing");
            entity.Property(e => e.Url).HasMaxLength(2083);
        });

        modelBuilder.Entity<DocCombineDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DocCombi__3214EC07C41786B9");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.DocCombine).WithMany(p => p.DocCombineDocuments)
                .HasForeignKey(d => d.DocCombineId)
                .HasConstraintName("FK__DocCombin__DocCo__22401542");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Documents_CategoryId");

            entity.HasIndex(e => e.CreatedBy, "IX_Documents_CreatedBy");

            entity.HasIndex(e => e.Url, "IX_Documents_Url");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CloudSlug).HasMaxLength(15);
            entity.Property(e => e.DocType).HasMaxLength(255);
            entity.Property(e => e.DocumentNumber).HasMaxLength(255);
            entity.Property(e => e.DocumentType).HasMaxLength(50);
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.IsCheckedIn).HasDefaultValue(0);
            entity.Property(e => e.IsCheckedOut).HasDefaultValue(0);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Status).HasMaxLength(150);

            entity.HasOne(d => d.Category).WithMany(p => p.Documents).HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<DocumentAuditTrail>(entity =>
        {
            entity.HasIndex(e => e.AssignToRoleId, "IX_DocumentAuditTrails_AssignToRoleId");

            entity.HasIndex(e => e.AssignToUserId, "IX_DocumentAuditTrails_AssignToUserId");

            entity.HasIndex(e => e.CreatedBy, "IX_DocumentAuditTrails_CreatedBy");

            entity.HasIndex(e => e.DocumentId, "IX_DocumentAuditTrails_DocumentId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Body).HasMaxLength(255);
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Type).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DocumentAuditTrails).HasForeignKey(d => d.CreatedBy);

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentAuditTrails)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DocumentComment>(entity =>
        {
            entity.HasIndex(e => e.CreatedBy, "IX_DocumentComments_CreatedBy");

            entity.HasIndex(e => e.DocumentId, "IX_DocumentComments_DocumentId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DocumentComments)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentComments).HasForeignKey(d => d.DocumentId);
        });

        modelBuilder.Entity<DocumentDeleteHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC0739A9768C");

            entity.ToTable("DocumentDeleteHistory");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<DocumentExternalLinkSharing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC07274E3296");

            entity.ToTable("DocumentExternalLinkSharing");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.ShareType).HasMaxLength(255);
        });

        modelBuilder.Entity<DocumentMetaData>(entity =>
        {
            entity.HasIndex(e => e.DocumentId, "IX_DocumentMetaDatas_DocumentId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentMetaData).HasForeignKey(d => d.DocumentId);
        });

        modelBuilder.Entity<DocumentRolePermission>(entity =>
        {
            entity.HasIndex(e => e.CreatedBy, "IX_DocumentRolePermissions_CreatedBy");

            entity.HasIndex(e => e.DocumentId, "IX_DocumentRolePermissions_DocumentId");

            entity.HasIndex(e => e.RoleId, "IX_DocumentRolePermissions_RoleId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CreatedbyUsername)
                .HasMaxLength(255)
                .HasColumnName("createdbyUsername");
            entity.Property(e => e.IsPassword).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("roleName");
            entity.Property(e => e.ShareType).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DocumentRolePermissions).HasForeignKey(d => d.CreatedBy);

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentRolePermissions)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DocumentShareLinkPermission>(entity =>
        {
            entity.ToTable("DocumentShareLinkPermission");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<DocumentTemplate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DocumentTemplate");

            entity.Property(e => e.CategoryId).HasMaxLength(255);
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasPrecision(0);
            entity.Property(e => e.CreatedbyUsername)
                .HasMaxLength(255)
                .HasColumnName("createdbyUsername");
            entity.Property(e => e.DeletedBy).HasMaxLength(255);
            entity.Property(e => e.DeletedDate).HasPrecision(0);
            entity.Property(e => e.DocumentName).HasMaxLength(255);
            entity.Property(e => e.DocumentType)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasPrecision(0);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.ValidFrom).HasPrecision(0);
            entity.Property(e => e.ValidTo).HasPrecision(0);
        });

        modelBuilder.Entity<DocumentToken>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC070356D280");

            entity.ToTable("DocumentType");

            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("Create_Date");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("Created_By");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(255)
                .HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DocSlug)
                .HasMaxLength(255)
                .HasColumnName("Doc_Slug");
            entity.Property(e => e.DocType)
                .HasMaxLength(255)
                .HasColumnName("Doc_Type");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(255)
                .HasColumnName("Modified_By");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("Modified_Date");
        });

        modelBuilder.Entity<DocumentUserPermission>(entity =>
        {
            entity.HasIndex(e => e.CreatedBy, "IX_DocumentUserPermissions_CreatedBy");

            entity.HasIndex(e => e.DocumentId, "IX_DocumentUserPermissions_DocumentId");

            entity.HasIndex(e => e.UserId, "IX_DocumentUserPermissions_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CreatedbyUsername)
                .HasMaxLength(255)
                .HasColumnName("createdbyUsername");
            entity.Property(e => e.IsPassword).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.ShareType).HasMaxLength(255);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .HasColumnName("userEmail");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("userName");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DocumentUserPermissions).HasForeignKey(d => d.CreatedBy);

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentUserPermissions)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DocumentVersion>(entity =>
        {
            entity.HasIndex(e => e.CreatedBy, "IX_DocumentVersions_CreatedBy");

            entity.HasIndex(e => e.DocumentId, "IX_DocumentVersions_DocumentId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CloudSlug).HasMaxLength(15);
            entity.Property(e => e.Comments).IsUnicode(false);
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.Version).HasColumnName("version");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DocumentVersions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentVersions).HasForeignKey(d => d.DocumentId);
        });

        modelBuilder.Entity<DocumentsDetail>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.PreprocessedText).HasColumnType("ntext");
            entity.Property(e => e.Tags).HasMaxLength(500);
            entity.Property(e => e.UploadedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<EmailRule>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ClientId).HasMaxLength(100);
            entity.Property(e => e.Condition).HasMaxLength(10);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Domain)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FromAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ToAddress).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<EmailSmtpsetting>(entity =>
        {
            entity.ToTable("EmailSMTPSettings");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.IsEnableSsl).HasColumnName("IsEnableSSL");
        });

        modelBuilder.Entity<EmailSyncSetting>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EmailSyncSetting");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsEnableSsl).HasColumnName("IsEnableSSL");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.SecretKey).HasMaxLength(255);
            entity.Property(e => e.SsoclientId)
                .HasMaxLength(255)
                .HasColumnName("SSOClientId");
        });

        modelBuilder.Entity<EmailSyncTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EmailSyncTable");

            entity.Property(e => e.FolderName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MessageId).HasMaxLength(255);
            entity.Property(e => e.SenderEmail).HasMaxLength(255);
            entity.Property(e => e.SenderName).HasMaxLength(255);
            entity.Property(e => e.Subject).HasMaxLength(500);
        });

        modelBuilder.Entity<FavouriteDocument>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
        });

        modelBuilder.Entity<FileStorageSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FileStor__3214EC07AB8F8778");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Path).HasMaxLength(255);
            entity.Property(e => e.PathName).HasMaxLength(255);
        });

        modelBuilder.Entity<FolderAuditTrail>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ActionName).HasMaxLength(25);
            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.CategoryType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClientId).HasMaxLength(100);
            entity.Property(e => e.FolderType).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
        });

        modelBuilder.Entity<FolderIndexingConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FolderIn__3214EC076D211471");

            entity.ToTable("FolderIndexingConfiguration");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SchemePattern).HasMaxLength(255);
            entity.Property(e => e.SchemePreview).HasMaxLength(255);
        });

        modelBuilder.Entity<FolderIndexingScheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FolderIn__3214EC07CAEFCB3E");

            entity.ToTable("FolderIndexingScheme");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SchemeName).HasMaxLength(255);
        });

        modelBuilder.Entity<FolderResetFrequency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FolderRe__3214EC0702514755");

            entity.ToTable("FolderResetFrequency");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MonthlyConfig).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.YearlyConfig).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<FolderSizingConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FolderSi__3214EC07A60DEDF1");

            entity.ToTable("FolderSizingConfiguration");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.DesktopUserCount)
                .HasDefaultValue(0)
                .HasColumnName("desktopUserCount");
            entity.Property(e => e.EmailUserCount)
                .HasDefaultValue(0)
                .HasColumnName("emailUserCount");
        });

        modelBuilder.Entity<FolderTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FolderTe__3214EC07C0E82C70");

            entity.ToTable("FolderTemplate");

            entity.HasIndex(e => e.ParentId, "idx_parentID");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CategoryId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.FolderPosition)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Permission).HasColumnName("permission");
        });

        modelBuilder.Entity<HalfYearlyReminder>(entity =>
        {
            entity.HasIndex(e => e.ReminderId, "IX_HalfYearlyReminders_ReminderId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Reminder).WithMany(p => p.HalfYearlyReminders).HasForeignKey(d => d.ReminderId);
        });

        modelBuilder.Entity<Hash>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Field }).HasName("PK_HangFire_Hash");

            entity.ToTable("Hash", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Field).HasMaxLength(100);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoice__3214EC072EAF7EAA");

            entity.ToTable("Invoice");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.ModeOfPayment).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.SupplierName).HasMaxLength(255);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Job");

            entity.ToTable("Job", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName").HasFilter("([StateName] IS NOT NULL)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.StateName).HasMaxLength(20);
        });

        modelBuilder.Entity<JobParameter>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Name }).HasName("PK_HangFire_JobParameter");

            entity.ToTable("JobParameter", "HangFire");

            entity.Property(e => e.Name).HasMaxLength(40);

            entity.HasOne(d => d.Job).WithMany(p => p.JobParameters)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_JobParameter_Job");
        });

        modelBuilder.Entity<JobQueue>(entity =>
        {
            entity.HasKey(e => new { e.Queue, e.Id }).HasName("PK_HangFire_JobQueue");

            entity.ToTable("JobQueue", "HangFire");

            entity.Property(e => e.Queue).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FetchedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<LinkDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LinkDocu__3214EC07029B9446");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.LinkName).HasMaxLength(255);
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Id }).HasName("PK_HangFire_List");

            entity.ToTable("List", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<LoginAudit>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Latitude).HasMaxLength(50);
            entity.Property(e => e.Longitude).HasMaxLength(50);
            entity.Property(e => e.RemoteIp)
                .HasMaxLength(50)
                .HasColumnName("RemoteIP");
        });

        modelBuilder.Entity<Nlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NLog__3214EC07E7C242BA");

            entity.ToTable("NLog");

            entity.Property(e => e.Callsite).HasMaxLength(300);
            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.Logged).HasColumnType("datetime");
            entity.Property(e => e.Logger).HasMaxLength(300);
            entity.Property(e => e.MachineName).HasMaxLength(200);
        });

        modelBuilder.Entity<NotificationAccessSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC0778A545C0");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.IsEmail).HasDefaultValue(false);
            entity.Property(e => e.IsSms)
                .HasDefaultValue(false)
                .HasColumnName("IsSMS");
            entity.Property(e => e.IsWeb).HasDefaultValue(true);
            entity.Property(e => e.IsWhatsapp).HasDefaultValue(false);
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<PersonalFolderSizeSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personal__3214EC074E35988A");

            entity.ToTable("PersonalFolderSizeSetting");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<QuarterlyReminder>(entity =>
        {
            entity.HasIndex(e => e.ReminderId, "IX_QuarterlyReminders_ReminderId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Reminder).WithMany(p => p.QuarterlyReminders).HasForeignKey(d => d.ReminderId);
        });

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.HasIndex(e => e.DocumentId, "IX_Reminders_DocumentId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ReminderType).HasMaxLength(50);

            entity.HasOne(d => d.Document).WithMany(p => p.Reminders).HasForeignKey(d => d.DocumentId);

            entity.HasMany(d => d.Users).WithMany(p => p.Reminders)
                .UsingEntity<Dictionary<string, object>>(
                    "ReminderUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Reminder>().WithMany().HasForeignKey("ReminderId"),
                    j =>
                    {
                        j.HasKey("ReminderId", "UserId");
                        j.ToTable("ReminderUsers");
                        j.HasIndex(new[] { "UserId" }, "IX_ReminderUsers_UserId");
                    });
        });

        modelBuilder.Entity<ReminderNotification>(entity =>
        {
            entity.HasIndex(e => e.ReminderId, "IX_ReminderNotifications_ReminderId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Reminder).WithMany(p => p.ReminderNotifications).HasForeignKey(d => d.ReminderId);
        });

        modelBuilder.Entity<ReminderScheduler>(entity =>
        {
            entity.HasIndex(e => e.DocumentId, "IX_ReminderSchedulers_DocumentId");

            entity.HasIndex(e => e.UserId, "IX_ReminderSchedulers_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Document).WithMany(p => p.ReminderSchedulers).HasForeignKey(d => d.DocumentId);

            entity.HasOne(d => d.User).WithMany(p => p.ReminderSchedulers).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<ReminderType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reminder__3214EC077DC41C73");

            entity.ToTable("ReminderType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.HasIndex(e => e.OperationId, "IX_RoleClaims_OperationId");

            entity.HasIndex(e => e.RoleId, "IX_RoleClaims_RoleId");

            entity.HasIndex(e => e.ScreenId, "IX_RoleClaims_ScreenId");

            entity.HasOne(d => d.Operation).WithMany(p => p.RoleClaims).HasForeignKey(d => d.OperationId);

            entity.HasOne(d => d.Role).WithMany(p => p.RoleClaims).HasForeignKey(d => d.RoleId);

            entity.HasOne(d => d.Screen).WithMany(p => p.RoleClaims).HasForeignKey(d => d.ScreenId);
        });

        modelBuilder.Entity<RoleCompanyLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CompanyLocation_1");

            entity.ToTable("RoleCompanyLocation");

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleCompanyLocations)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_CompanyLocation_Roles");
        });

        modelBuilder.Entity<RoleFolderPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleFold__3214EC07A5167D46");

            entity.ToTable("RoleFolderPermission");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CreatedbyUsername)
                .HasMaxLength(255)
                .HasColumnName("createdbyUsername");
            entity.Property(e => e.IsAllowDocumentUpload).HasColumnName("isAllowDocumentUpload");
            entity.Property(e => e.IsAllowShare).HasColumnName("isAllowShare");
            entity.Property(e => e.IsAllowSubFolderCreate).HasColumnName("isAllowSubFOlderCreate");
            entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            entity.Property(e => e.IsShareSubFolder).HasColumnName("isShareSubFolder");
            entity.Property(e => e.IsTimeBound).HasColumnName("isTimeBound");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<Schema>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("PK_HangFire_Schema");

            entity.ToTable("Schema", "HangFire");

            entity.Property(e => e.Version).ValueGeneratedNever();
        });

        modelBuilder.Entity<Screen>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<ScreenOperation>(entity =>
        {
            entity.HasIndex(e => e.OperationId, "IX_ScreenOperations_OperationId");

            entity.HasIndex(e => e.ScreenId, "IX_ScreenOperations_ScreenId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Default).HasDefaultValue(false);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Operation).WithMany(p => p.ScreenOperations).HasForeignKey(d => d.OperationId);

            entity.HasOne(d => d.Screen).WithMany(p => p.ScreenOperations).HasForeignKey(d => d.ScreenId);
        });

        modelBuilder.Entity<SendEmail>(entity =>
        {
            entity.HasIndex(e => e.DocumentId, "IX_SendEmails_DocumentId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Document).WithMany(p => p.SendEmails).HasForeignKey(d => d.DocumentId);
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Server");

            entity.ToTable("Server", "HangFire");

            entity.HasIndex(e => e.LastHeartbeat, "IX_HangFire_Server_LastHeartbeat");

            entity.Property(e => e.Id).HasMaxLength(200);
            entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Value }).HasName("PK_HangFire_Set");

            entity.ToTable("Set", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => new { e.Key, e.Score }, "IX_HangFire_Set_Score");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Value).HasMaxLength(256);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<ShareLinkPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ShareLin__3214EC07CCF8F8F6");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.DeletedBy).HasMaxLength(255);
            entity.Property(e => e.DocumentId).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ShareKey).HasMaxLength(255);
            entity.Property(e => e.ShareLink).HasMaxLength(255);
            entity.Property(e => e.ShareType).HasMaxLength(255);
        });

        modelBuilder.Entity<Smssetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SMSSetti__3214EC0772A2245B");

            entity.ToTable("SMSSettings");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
            entity.Property(e => e.MobileNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Id }).HasName("PK_HangFire_State");

            entity.ToTable("State", "HangFire");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Job).WithMany(p => p.States)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_State_Job");
        });

        modelBuilder.Entity<TCompanyMaster>(entity =>
        {
            entity.ToTable("tCompanyMaster");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Address1).HasMaxLength(50);
            entity.Property(e => e.Address2).HasMaxLength(50);
            entity.Property(e => e.Address3).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.CountryCode).HasMaxLength(100);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Division)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.EmployerCrnoC)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EmployerCRNo_c");
            entity.Property(e => e.IsTmultiProject)
                .HasDefaultValue(false)
                .HasColumnName("IsTMultiProject");
            entity.Property(e => e.MaxAirTicketBalance).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PayerCrnoC)
                .HasMaxLength(100)
                .HasColumnName("PayerCRNo_c");
            entity.Property(e => e.PhoneNum).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.Vertical)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Zip).HasMaxLength(10);
        });

        modelBuilder.Entity<TLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tLocatio__3214EC2752CE8F0B");

            entity.ToTable("tLocation");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Complist).HasMaxLength(150);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OnShoreOffShore)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccessFailedCount).HasMaxLength(100);
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DialCode).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.SecurityStamp).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(256);
            entity.Property(e => e.UserSign).HasColumnType("text");
            entity.Property(e => e.UserWatermark).HasColumnType("text");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_UserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.HasIndex(e => e.OperationId, "IX_UserClaims_OperationId");

            entity.HasIndex(e => e.ScreenId, "IX_UserClaims_ScreenId");

            entity.HasIndex(e => e.UserId, "IX_UserClaims_UserId");

            entity.HasOne(d => d.Operation).WithMany(p => p.UserClaims).HasForeignKey(d => d.OperationId);

            entity.HasOne(d => d.Screen).WithMany(p => p.UserClaims).HasForeignKey(d => d.ScreenId);

            entity.HasOne(d => d.User).WithMany(p => p.UserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<UserFolder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserFold__3214EC076E586122");

            entity.ToTable("UserFolder");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CompanySeal).HasColumnType("text");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsSplashScreen).HasColumnName("isSplashScreen");
        });

        modelBuilder.Entity<UserFolderDragAndDrop>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserFolderDragAndDrop");

            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.FolderPosition).IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserFolderPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserFold__3214EC07FDF03A45");

            entity.ToTable("UserFolderPermission");

            entity.HasIndex(e => new { e.UserId, e.FolderId }, "idx_UserFolder");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.CreatedbyUsername)
                .HasMaxLength(255)
                .HasColumnName("createdbyUsername");
            entity.Property(e => e.FolderPosition)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsAllowDocumentUpload).HasColumnName("isAllowDocumentUpload");
            entity.Property(e => e.IsAllowFolderCreate).HasColumnName("isAllowFolderCreate");
            entity.Property(e => e.IsAllowShare).HasColumnName("isAllowShare");
            entity.Property(e => e.IsAllowSubFolderCreate).HasColumnName("isAllowSubFOlderCreate");
            entity.Property(e => e.IsShareSubFolder).HasColumnName("isShareSubFolder");
            entity.Property(e => e.IsTimeBound).HasColumnName("isTimeBound");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .HasColumnName("userEmail");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_UserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.UserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.HasIndex(e => e.DocumentId, "IX_UserNotifications_DocumentId");

            entity.HasIndex(e => e.UserId, "IX_UserNotifications_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);

            entity.HasOne(d => d.Document).WithMany(p => p.UserNotifications).HasForeignKey(d => d.DocumentId);
        });

        modelBuilder.Entity<UserNotificationAccessSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserNoti__3214EC07A3FEA606");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.IsEmail).HasDefaultValue(false);
            entity.Property(e => e.IsSms)
                .HasDefaultValue(false)
                .HasColumnName("IsSMS");
            entity.Property(e => e.IsWeb).HasDefaultValue(true);
            entity.Property(e => e.IsWhatsapp).HasDefaultValue(false);
        });

        modelBuilder.Entity<UserQuery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserQuer__3214EC07C4084869");

            entity.HasIndex(e => e.TicketNumber, "UQ__UserQuer__CBED06DA8C744C13").IsUnique();

            entity.HasIndex(e => e.TicketNumber, "UQ__UserQuer__CBED06DAA81AF9F1").IsUnique();

            entity.Property(e => e.App).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TicketNumber).HasMaxLength(100);
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<VersionScreen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VersionS__3214EC0715E7E559");

            entity.ToTable("VersionScreen");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VersionIsDeleted).HasDefaultValue(false);
            entity.Property(e => e.VersionNumber).HasMaxLength(25);
            entity.Property(e => e.VersionTitle).HasMaxLength(255);
        });

        modelBuilder.Entity<WhatsappLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__whatsapp__3214EC076DF4E6AD");

            entity.ToTable("whatsapp_log");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ClientId).HasMaxLength(50);
            entity.Property(e => e.CompanyId).HasMaxLength(50);
            entity.Property(e => e.SelectedCompany).HasColumnName("selectedCompany");
        });

        modelBuilder.Entity<WhatsappSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Whatsapp__3214EC0789DDA2BE");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(255);
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
