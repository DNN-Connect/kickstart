<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ProjectsDetail.ascx.vb" Inherits="Connect.Modules.Kickstart.ProjectsDetail" %>
<%@ Import Namespace="Connect.Modules.Kickstart" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

<div class="kickstart-projectdetails"></div>

<script type="text/javascript">

    var mid = <%= ModuleId%>;
    var pid = <%= ProjectId%>;
    var pageNo = 1;
    var recordsCount = 10;
    var sortCol = 'DateCreated_D';

    var waitText = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("waitText")%>;'
    var isHiddenText = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("cmdApproveProject")%>';
    var isVisibleText = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("cmdUnApproveProject")%>';
    var isLockedText = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("cmdUnLockProject")%>';
    var isUnLockedText = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("cmdLockProject")%>';
    var isDeletedText = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("cmdUnDeleteProject")%>';
    var isUnDeletedText = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("cmdDeleteProject")%>';
    var submitCommentText = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("PostComment")%>';
    var subscriptionChangedHeading = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("SubscriptionChangedHeading")%>';
    var currentCommentId = -1;

    function setupKickstartPage(){

        loadProject(mid, pid, refreshProject);

    }

    function setProjectVisibilityButton(visibilityClass) {

        var link = $("a.visibility");
        link.removeClass("isvisible");
        link.removeClass("ishidden");
        link.addClass(visibilityClass);

        loadProjectStatus(mid, pid, processStatusResponse);
        if(visibilityClass == 'isvisible') { enableLink(link, isVisibleText); } else { enableLink(link, isHiddenText); }
        
        attachStatusActions();     
        
    }

    function setProjectLockButton(visibilityClass) {

        var link = $("a.locking");
        link.removeClass("islocked");
        link.removeClass("isunlocked");
        link.addClass(visibilityClass);

        loadProjectStatus(mid, pid, processStatusResponse);
        if(visibilityClass == 'isunlocked') { enableLink(link, isUnLockedText); } else { enableLink(link, isLockedText); }
        attachStatusActions();     
        
    }

    function setProjectDeleteButton(visibilityClass) {


        var link = $("a.deleting");
        link.removeClass("isdeleted");
        link.removeClass("isundeleted");
        link.addClass(visibilityClass);

        loadProjectStatus(mid, pid, processStatusResponse);
        if(visibilityClass == 'isundeleted') { enableLink(link, isUnDeletedText); } else { enableLink(link, isDeletedText); }
        attachStatusActions();     
        
    }

    function setProjectDeleteButton(visibilityClass) {


        var link = $("a.deleting");
        link.removeClass("isdeleted");
        link.removeClass("isundeleted");
        link.addClass(visibilityClass);

        loadProjectStatus(mid, pid, processStatusResponse);
        if(visibilityClass == 'isundeleted') { enableLink(link, isUnDeletedText); } else { enableLink(link, isDeletedText); }
        attachStatusActions();     
        
    }

    function refreshComments(data) {
        
        var container;
        if (currentCommentId == -1){
            container = $(".kickstart-commentlist");
        } else {
            var id = '#Comment-' + currentCommentId;  
            container = $(id).find(".kickstart-commentitem-children").first();
        }
        container.append(data).slideDown("slow");
        var link = $("a.submitcomment");
        enableLink(link, submitCommentText);
        resetCurrentComment();
        attachStatusActions();
    }

    function refreshSingleComment(data) {
        
        var id = '#Comment-' + currentCommentId;        
        var container = $(id);        
        if (container) {
            container.replaceWith(data);
        }
        attachStatusActions();
        currentCommentId = -1;
    }

    function refreshEditedComment(data) {
        
        var id = '#Comment-' + currentCommentId;        
        var container = $(id);        
        if (container) {
            container.html(data);
        }
        var link = $("a.submitcomment");
        enableLink(link, submitCommentText);
        resetCurrentComment();
        attachStatusActions();
        currentCommentId = -1;
    }

    function refreshProject(data) {
        
        $(".kickstart-projectdetails").hide().html(data).slideDown("slow");
        attachStatusActions();
    }

    function processStatusResponse(data) {
        
        $(".kickstart-project-status").hide().html(data).slideDown("fast");
        attachStatusActions();

    }

    function attachStatusActions() {
        $('.dnnSecondaryAction.visibility.ishidden').off("click");
        $('.dnnSecondaryAction.visibility.isvisible').off("click");
        $('.dnnSecondaryAction.locking.islocked').off("click");
        $('.dnnSecondaryAction.locking.isunlocked').off("click");
        $('.dnnSecondaryAction.deleting.isdeleted').off("click");
        $('.dnnSecondaryAction.deleting.isundeleted').off("click");
        $('.dnnPrimaryAction.submitcomment').off("click");
        $('a.kickstart-commentitem-like').off("click");
        $('a.kickstart-commentitem-reply').off("click");
        $('a.kickstart-commentitem-edit').off("click");
        $('a.kickstart-commentitem-delete').off("click");
        $('a.kickstart-projectitem-like').off("click");
        $('a.kickstart-managesubscription-release').off("click");
        $('a.kickstart-managesubscription-comment').off("click");

        $('.dnnSecondaryAction.visibility.ishidden').click(function(){
            disableLink(this);
            setProjectVisibilityStatus(mid, pid, 1, setProjectVisibilityButton);
            return false;
        });
        $('.dnnSecondaryAction.visibility.isvisible').click(function(){
            disableLink(this);
            setProjectVisibilityStatus(mid, pid, 0, setProjectVisibilityButton);
            return false;
        });
        $('.dnnSecondaryAction.locking.islocked').click(function(){
            disableLink(this);
            setProjectLockStatus(mid, pid, 0, setProjectLockButton);
            return false;
        });
        $('.dnnSecondaryAction.locking.isunlocked').click(function(){
            disableLink(this);
            setProjectLockStatus(mid, pid, 1, setProjectLockButton);
            return false;
        });
        $('.dnnSecondaryAction.deleting.isdeleted').click(function(){
            disableLink(this);
            setProjectDeletedStatus(mid, pid, 0, setProjectDeleteButton);
            return false;
        });
        $('.dnnSecondaryAction.deleting.isundeleted').click(function(){
            disableLink(this);
            setProjectDeletedStatus(mid, pid, 1, setProjectDeleteButton);
            return false;
        });
        $('.dnnPrimaryAction.submitcomment').click(function(){
            disableLink(this);
            var comment = $(".kickstart-commentcontrol").val();
            addComment(mid, pid, comment, currentCommentId, refreshComments);          
            return false;
        });
        $('a.kickstart-commentitem-like').click(function(){
            currentCommentId = $(this).prop("rel");
            likeComment(mid, pid, currentCommentId, refreshSingleComment);
            return false;
        });
        $('a.kickstart-commentitem-unlike').click(function(){
            currentCommentId = $(this).prop("rel");
            unlikeComment(mid, pid, currentCommentId, refreshSingleComment);
            return false;
        });
        $('a.kickstart-commentitem-reply').click(function(){
            currentCommentId = $(this).prop("rel");
            setReply();
        });
        $('a.kickstart-commentitem-edit').click(function(){
            currentCommentId = $(this).prop("rel");
            setEdit();
        });
        $('a.kickstart-commentitem-delete').click(function(){
            currentCommentId = $(this).prop("rel");
            deleteComment(mid, pid, currentCommentId, refreshSingleComment);
            return false;
        });
        $('a.kickstart-projectitem-like').click(function(){
            likeProject(mid, pid, refreshProject);
            return false;
        });
        $('a.kickstart-projectitem-unlike').click(function(){
            unlikeProject(mid, pid, refreshProject);
            return false;
        });
        $('a.kickstart-memberlist-popuplink').click(function(){
            var list = $(this).next();
            showMemberList(list);
            return false;
        });
        $('a.kickstart-managesubscription-release').click(function(){
            changeReleaseSubscriptionStatus(mid, pid, processSubscriptionChangeResult);
            return false;
        });
        $('a.kickstart-managesubscription-comment').click(function(){
            changeCommentSubscriptionStatus(mid, pid, processSubscriptionChangeResult);
            return false;
        });
    }

    function processSubscriptionChangeResult(data) {
        $.dnnAlert({ 
            title: subscriptionChangedHeading,
            okText: 'OK', 
            text: data
        });
        loadProject(mid, pid, refreshProject);
    }

    function showMemberList(container){
        container.toggle();
    }

    function resetCurrentComment() {
        currentCommentId = -1;
        $(".kickstart-quote").html('');
        $(".kickstart-commentcontrol").val('');
    }

    function setReply() {
        
        var sourceId = '#Comment-' + currentCommentId;        
        var sourceContainer = $(sourceId);   
        var sourceIntro = sourceContainer.find(".kickstart-commentitem-head").html();
        var sourceText = sourceContainer.find(".kickstart-commentitem-content").html();
        var targetContainer = $(".kickstart-quote");
        targetContainer.html(sourceIntro + ' ' + sourceText);
        $(".kickstart-commentcontrol").val('');
    }

    function setEdit() {
        
        var sourceId = '#Comment-' + currentCommentId;        
        var sourceContainer = $(sourceId);   
        var sourceIntro = sourceContainer.find(".kickstart-commentitem-head").html();
        var sourceText = sourceContainer.find(".kickstart-commentitem-content").html().trim();
        var targetContainer = $(".kickstart-quote");
        targetContainer.html(sourceIntro);
        var targetControl = $(".kickstart-commentcontrol");
        targetControl.val(sourceText);

        $('.dnnPrimaryAction.submitcomment').off("click");
        $('.dnnPrimaryAction.submitcomment').click(function(){
            var comment = targetControl.val();
            editComment(mid, pid, comment, currentCommentId, refreshEditedComment);          
            return false;
        });
    }

    (function ($, Sys) {
        $(document).ready(function () {
            setupKickstartPage();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setupKickstartPage();
            });
        });
    }($, window.Sys));

</script>