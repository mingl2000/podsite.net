**Video**
    https://www.youtube.com/watch?v=a9eR1xsfvHg

**Init a project**
    uvx --from git+https://github.com/github/spec-kit.git specify init podsite

**Update constitution.md**

    * constitution.md
    * GPT-5 mini: 
    * Fill the constitution with the bare minimum requirements for a static web app based on the template.
    * outcome: 
        constitution.md is updated

    * Agent
    * GTP-5 mini
    * For things that need clarification, use the best guess you think is reasonable. Update acceptance checklist after.
     * outcome: 
        constitution.md is updated
**build spec**
    * Agent
    * GTP-5 mini
    * /speckit.specify  I am building a modern podcast website. I want it to look sleek, something that would stand out. Should have a landing page with one featured episode. There should be an episodes page, an about page, and a FAQ page. Should have 20 episodes, and the data is mocked - you do not need to pull anything from any real feed.
    *   outcome: spec.md
    
**create plan**
    * spec.md
    * GTP-5:
    * /speckit.plan I am going to use Next.js with static site configuration, no databases - data is embedded in the content for the mock episodes. Site is responsive and ready for mobile.
**Create tasks**
    * plan.md
    * agent
    * /speckit.tasks break this down into tasks
    * outcome: tasks.md

**Implemement**
    * tasks.md
    * Agent
    * Claude Haiku 4.5:
    * Implement the tasks for this project, and update the task list as you go.
    * outcome: the web site code.
    
**Build**
    cmd:
        npm run build
        npm run dev