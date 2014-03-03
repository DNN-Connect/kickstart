<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ProjectsList.ascx.vb" Inherits="Connect.Modules.Kickstart.ProjectsList" %>


<div class="kickstart-projectlist"></div>
<div class="kickstart-paging"></div>

<script type=""text/javascript"">

    var subscriptionChangedHeading = '<%= Connect.Modules.Kickstart.Utilities.GetSharedJSSafeResource("SubscriptionChangedHeading")%>';
    var mid = <%= ModuleId%>;
    var pageNo = 1;
    var recordsCount = 3;
    var sortCol = 'DateCreated_D';
    var slideDirection = 'down';
    var effect = 'blind'; 
    var options = { direction: slideDirection }; 
    var duration = 400;

    function loadData() {
        loadProjects(mid, pageNo, recordsCount, sortCol, processListHtml);
        loadProjectsPaging(mid, recordsCount, processPagingHtml);
    }

    function processListHtml(data) {

        $(".kickstart-projectlist").hide().html(data).fadeToggle( "fast", "linear" );
        $('.kickstart-paging a').removeClass('active');
        $("a.pagelink[rel='" + pageNo +"']").toggleClass('active');

        $('a.kickstart-managesubscription-project').off("click");
        $('a.kickstart-managesubscription-project').click(function(){
            changeProjectListSubscriptionStatus(mid, processSubscriptionChangeResult);
            return false;
        });

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