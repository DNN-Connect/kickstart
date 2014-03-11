<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ProjectsList.ascx.vb" Inherits="Connect.Modules.Kickstart.ProjectsList" %>

<div class="kickstart-container dnnClear">
    <div class="kickstart-projectsloader"></div>
    <div class="kickstart-paging"></div>
    <div class="kickstart-loading"><img src="/Desktopmodules/Connect/Kickstart/images/ajax-loader.gif" /></div>    
</div>

<script type=""text/javascript"">

    var subscriptionChangedHeading = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("SubscriptionChangedHeading")%>';
    var showApprovedText = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("cmdViewApproved")%>';
    var showAllText = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("cmdViewAll")%>';

    var mid = <%= ModuleId%>;
    var userid = <%= UserId %>;
    var pageNo = 1;
    var recordsCount = 3;
    var sortCol = 'DateCreated_D';
    var slideDirection = 'down';
    var effect = 'blind'; 
    var options = { direction: slideDirection }; 
    var duration = 400;
    var isVisible = 1;
    var isDeleted = 0;
    var createdBy = -1;
    var leadBy = -1;
    var participantId = -1;
    var btnCurrent = 'cmdviewAll';
    


    function loadData() {        
        loadProjects(mid, pageNo, recordsCount, sortCol, isVisible, isDeleted, createdBy, leadBy, participantId, processListHtml);
        loadProjectsPaging(mid, recordsCount, processPagingHtml);        
    }

    function toggleViewButtons() {

        $(".viewButtons li").each(function(){
            $(this).find("a").removeClass('active');
        });
        if (btnCurrent) {
            $('#' + btnCurrent).addClass('active');
        }
    }

    function processListHtml(data) {

        $(".kickstart-projectsloader").html(data);

        $('.kickstart-paging a').removeClass('active');
        $("a.pagelink[rel='" + pageNo +"']").toggleClass('active');

       
        $('a.kickstart-managesubscription-project').off("click");
        $('a.kickstart-view-myideas').off("click");
        $('a.kickstart-view-unapproved').off("click");
        $('a.kickstart-view-deleted').off("click");
        $('a.kickstart-view-myparticipation').off("click");
        $('a.kickstart-view-all').off("click");

        $('a.kickstart-managesubscription-project').click(function(){
            changeProjectListSubscriptionStatus(mid, processSubscriptionChangeResult);
            return false;
        });
        $('a.kickstart-view-myideas').click(function(){
            isVisible = -1;
            createdBy = userid;
            leadBy =-1;
            participantId = -1;
            isDeleted = -1;
            btnCurrent = this.id;
            loadData();
            
            return false;
        });
        $('a.kickstart-view-unapproved').click(function(){
            isVisible = 0;
            createdBy = -1;
            leadBy =-1;
            participantId = -1;
            isDeleted = -1;
            btnCurrent = this.id;
            loadData();
            
            return false;
        });
        $('a.kickstart-view-deleted').click(function(){
            isVisible = -1;
            createdBy = -1;
            leadBy =-1;
            participantId = -1;
            isDeleted = 1;
            btnCurrent = this.id;
            loadData();
           
            return false;
        });
        $('a.kickstart-view-myparticipation').click(function(){
            isVisible = -1;
            createdBy = -1;
            leadBy =-1;
            participantId = userid;
            isDeleted = -1;
            btnCurrent = this.id;
            loadData();
            
            return false;
        });
        $('a.kickstart-view-all').click(function(){
            isVisible = 1;
            isDeleted = 0;
            createdBy = -1;
            leadBy = -1;
            participantId = -1;
            btnCurrent = this.id;
            loadData();
            
            return false;
        });

        toggleViewButtons();
        decrement();
    }

    function processSubscriptionChangeResult(data) {
        $.dnnAlert({ 
            title: subscriptionChangedHeading,
            okText: 'OK', 
            text: data
        });
        loadData();
    }

    function processPagingHtml(data) {

        $(".kickstart-paging").html(data);

        $('.kickstart-paging a').each(function() {
            $(this).off("click");
            $(this).click(function(){
                pageNo = $(this).prop("rel");
                loadProjects(mid, pageNo, recordsCount, sortCol, processListHtml);       
            })
        });

    }

    (function ($, Sys) {
        $(document).ready(function () {
            loadData();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                loadData();
            });
        });
    }(jQuery, window.Sys));

</script>