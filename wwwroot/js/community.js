const CommunityPosts = document.getElementById("x-posts");
let PostContent = ``;
for (let i = 0; i < 6; i++) {
  PostContent += `
    <div class="post bg-white rounded-md shadow-md md:p-7 p-4 mb-4 border border-gray-300">
      <div class="post-head flex justify-between items-center">
        <div class="flex gap-3 cursor-pointer">
          <div class="w-10 h-10 mx-auto rounded-full bg-gray-200 flex items-center justify-center text-blue-950 font-bold text-lg box-border">FA</div>
          <div>
            <h6 class="text-sm tracking-wide">Furqan Ahmad</h6>
            <p class="text-xs text-gray-500">6 min ago</p>
          </div>
        </div>
        <a class="community-button-hover bg-blue-800 text-white inline-block text-sm p-2 rounded-lg border border-blue-800 shadow-md" href="/community/post-${i + 1}">View Post</a>
      </div>
      <div class="post-body mt-3">
        <p class="post-content text-sm text-justify">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Dicta eius ipsum eos itaque atque
          recusandae hic
          possimus libero voluptates non perferendis iusto temporibus vitae quod et, dolor beatae iure
          quidem.
        </p>
      </div>
      <div class="post-footer flex justify-between items-center mt-4 gap-3">
        <input type="text" name="comment" id="${i + 1}"
          class="w-full bg-gray-100 px-4 py-2 rounded-2xl outline-none text-sm"
          placeholder="Add a comment...">
          <button type="button" class="flex text-sm rounded-full bg-blue-800 text-white p-2 pr-[10px] community-button-hover"><img
            src="/images/publish.svg" alt="Send"></button>
      </div>

    </div>
  `;
}
CommunityPosts.innerHTML = PostContent;


const LatestUpdates = document.getElementById("latest-updates");
let UpdatesContent = ``;
for (let i = 0; i < 8; i++) {
  UpdatesContent += `
    <div class="update-item rounded-md shadow-md p-4 mb-2 border border-gray-300">
      <h4 class="text-sm font-semibold">New Feature: Dark Mode</h4>
      <p class="text-xs text-gray-500">We have added a new dark mode feature for better readability.</p>
    </div>
  `;
}
LatestUpdates.innerHTML = UpdatesContent;


const TopContributors = document.getElementById("top-contributors");
let ContributorsContent = ``;
for (let i = 0; i < 6; i++) {
  ContributorsContent += `
    <div class="contributor-item rounded-md shadow-md p-4 mb-2 border border-gray-300">
      <h4 class="text-sm font-semibold">Furqan Ahmad</h4>
      <p class="text-xs text-gray-500">100 Posts</p>
    </div>
  `;
}
TopContributors.innerHTML = ContributorsContent;
