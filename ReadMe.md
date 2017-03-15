Send Me!
============
An application that allows medical students to receive and track 
funds raised for over-seas practicums.

_A collaborative project between [Michael Anderson](https://github.com/Manderson566) and [Kristin Sindorf](https://github.com/ksindorf000)._

---------------

### The Motivation

#### The Global Health Initiative from the World Health Organization

WHO's Global School Health Initiative, launched in 1995, seeks to mobilise and strengthen health promotion and education activities at the local, national, regional and global levels. The Initiative is designed to improve the health of students, school personnel, families and other members of the community through schools.

**Research to improve school health programmes**: Evaluation research and expert opinion is analyzed and consolidated to describe the nature and effectiveness of school health programmes.

**Building capacity to advocate for improved school health programmes**: technical documents are generated that consolidate research and expert opinion about the nature, scope and effectiveness of school health programmes.Each advocacy document makes a strong case for addressing an important health problem, identifies components of a comprehensive school health programme, and provides guidance in integrating the issue into the components.

**Strengthening national capacities**: collaboration between health and education agencies is fostered and countries are helped to develop strategies and programmes to improve health through schools. Pilot projects implemented by the GSHI and partners include Helminth Interventions with China in 1996, HIV/STI Prevention in China in 1997, and Health-Promoting Schools/Health Insurance in Vietnam in 1998.

### The Problem
Medical students need to be able to raise and track funds for their
overseas practicums. Faculty and staff need to be able to track students'
fund-raising progress. However, since there is no standard system for
students to use, faculty and staff end up having to keep up with multiple
applications depending on each student's tracking method of choice. 

### The Solution
Send Me! is a secure, user-friendly application that will allow students to
create profiles to which donors can navigate in order to donate funds to the 
student's trip. The student's profile will display their active trip and the
progress they have made towards their monetary goal. 

Students will have to verify their school email address and so will be affiliated 
with their school in the application. Each school will have a dedicated page where 
the progress of their students' fund-raising is displayed. This will make it easy
for faculty and staff to keep up with all of their students in one place. 

As an added bonus,this will allow Alumni of the school who wish to donate to 
current students the opportunity to do so without having to reach out to the 
school's administration to find out where to send funds.

### Minimal Viable Product

 1. Students can create accounts with verified school email addresses
 2. Users can navigate to School Profile to see list of students with active trips
 3. Donors can navigate to Student profiles and donate funds through Stripe API
 4. Students can see list of Donors and contact info

### Milestones

 - Week One:

    1. Student accounts
        - Verified school email
        - Profile Pic upload
        - Trip creation

    2. Functioning Stripe integration
        - Donors can successfully post amounts to trip
        - Populates Donor List on Trip Details (viewable to student only)


 - Week Two:

    1. Interactive Progress Bar
        - Fills according to percentage of completion
        - Hover shows percentage
    2. Manual donations (cash/checks)


 - Week Three:

    1. Thank you emails


### Stretch Goals (ordered by priority)

 1. Multiple Schools
 2. School map to display all active trips
 3. [Route("/{username}")]
 4. Progress bar overlay on map (airplane icon moves toward destination)
 5. Read Only Mobile App
 

### Super Stretch Goals (ordered by Priority)

 1. Donations on Mobile
 2. Push Notifications on Mobile
 3. Thank You's on Mobile
