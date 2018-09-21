from flask import Flask, render_template
import random

app = Flask(__name__)

# list of cat images
images = [
    "https://24.media.tumblr.com/tumblr_lstmtgYJVx1r4xjo2o1_500.jpg",
    "https://25.media.tumblr.com/tumblr_lyxvheqwhZ1r6b7kmo1_500.jpg",
    "https://25.media.tumblr.com/tumblr_m11oqlcb8s1r9qf7so1_500.jpg",
    "https://24.media.tumblr.com/tumblr_m25zjtZy5x1qabm53o1_500.jpg",
    "https://24.media.tumblr.com/qgIb8tERipvdnd9870HESxL3o1_500.jpg",
    "https://25.media.tumblr.com/tumblr_lparluSFjh1qij6yko1_500.jpg",
    "https://25.media.tumblr.com/tumblr_ltmntg3gCR1qcmkeso1_500.jpg",
    "https://28.media.tumblr.com/tumblr_lh6z4gpRNB1qfyzelo1_500.jpg",
    "https://25.media.tumblr.com/RKwjoIoFXlyx98elpml4ZY98o1_400.jpg",
    "https://26.media.tumblr.com/tumblr_m35gs01pWX1qhwmnpo1_500.jpg",
    "https://30.media.tumblr.com/tumblr_m3glwlj6J71r73wdao1_400.jpg",
]

@app.route('/')
def index():
    url = random.choice(images)
    return render_template('index.html', url=url)

if __name__ == "__main__":
    app.run(host="0.0.0.0")
