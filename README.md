# Personal-Website

For this Project I built a personal website, intended for recruiters and Interviewers.

## Requirements

1. Website with two sections: a public static section using HTML5, CSS3, and ES6, and a password protected section using Asp.Net Core.

2. Contents are academic and professional information, portfolio projects, statement of interests, career preferences, including photos, diagrams, code samples, and any other information that may be relevant.

3. The site provides the ability to accept comments, notes, and requests from recruiters that are authenticated by the site.

4. The private inputs, cited in the previous requirements, are viewable only by you and the authenticated user for whom they are intended and any comments or assessments the user may have provided.

5. Contents are partitioned into topic blocks, each of which may have text, diagrams, photos, and commentary.

6. The site provides and a webservice built using Asp.Net Web API with the ability to upload, edit, and delete projects, resumes, and topic-blocks.

7. Clients for WebService

## Usage

1. The zip file does not contain a database, so you will have to rebuild it. </br>
   First, unzip and build the project, but don't run it yet.
2. Delete all content from the Migrations folder.
3. Select top menu Tools > NuGet Package Manager > Package Manager Console.
4. At the Console prompt, PM>, type add-migration [Initial] where [...] is the name of the migration
5. At the Console prompt type update-database

Now you will be able to run the project and add content.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
