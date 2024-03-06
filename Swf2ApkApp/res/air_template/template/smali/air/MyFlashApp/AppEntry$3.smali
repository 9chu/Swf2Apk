.class Lair/{{ProjectName}}/AppEntry$3;
.super Ljava/lang/Object;
.source "AppEntry.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lair/{{ProjectName}}/AppEntry;->launchAIRService()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lair/{{ProjectName}}/AppEntry;


# direct methods
.method constructor <init>(Lair/{{ProjectName}}/AppEntry;)V
    .locals 0

    .prologue
    .line 293
    iput-object p1, p0, Lair/{{ProjectName}}/AppEntry$3;->this$0:Lair/{{ProjectName}}/AppEntry;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 2
    .param p1, "name"    # Landroid/content/ComponentName;
    .param p2, "service"    # Landroid/os/IBinder;

    .prologue
    .line 299
    iget-object v0, p0, Lair/{{ProjectName}}/AppEntry$3;->this$0:Lair/{{ProjectName}}/AppEntry;

    invoke-virtual {v0, p0}, Lair/{{ProjectName}}/AppEntry;->unbindService(Landroid/content/ServiceConnection;)V

    .line 302
    iget-object v0, p0, Lair/{{ProjectName}}/AppEntry$3;->this$0:Lair/{{ProjectName}}/AppEntry;

    invoke-static {v0}, Lair/{{ProjectName}}/AppEntry;->access$300(Lair/{{ProjectName}}/AppEntry;)V

    .line 303
    iget-object v0, p0, Lair/{{ProjectName}}/AppEntry$3;->this$0:Lair/{{ProjectName}}/AppEntry;

    const/4 v1, 0x0

    invoke-static {v0, v1}, Lair/{{ProjectName}}/AppEntry;->access$400(Lair/{{ProjectName}}/AppEntry;Z)V

    .line 305
    invoke-static {}, Lair/{{ProjectName}}/AppEntry;->access$500()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 307
    iget-object v0, p0, Lair/{{ProjectName}}/AppEntry$3;->this$0:Lair/{{ProjectName}}/AppEntry;

    invoke-static {v0}, Lair/{{ProjectName}}/AppEntry;->access$600(Lair/{{ProjectName}}/AppEntry;)V

    .line 314
    :goto_0
    return-void

    .line 312
    :cond_0
    invoke-static {}, Lair/{{ProjectName}}/AppEntry;->access$700()V

    goto :goto_0
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 0
    .param p1, "name"    # Landroid/content/ComponentName;

    .prologue
    .line 319
    return-void
.end method
