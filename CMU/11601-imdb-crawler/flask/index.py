from flask import Flask, render_template, session, redirect, url_for, flash
from flask.ext.bootstrap import Bootstrap
from flask.ext.moment import Moment
from datetime import datetime
from flask.ext.wtf import Form
from wtforms import StringField, SubmitField
from wtforms.validators import Required

from main import *
from search_page import parseSearchPage
from movie_page import parseMoviePage
from cast_page import parseCastPage
from sqllink import DBConnector

global SEED_URL
global HOST_URL
global db

app = Flask(__name__)
app.config['SECRET_KEY'] = 'GODUCKYOURSELF'
bootstrap = Bootstrap(app)
moment = Moment(app)


db = DBConnector()

HOST_URL = "http://www.imdb.com"

db.createTables()


class KeywordsForm(Form):
    keywords = StringField('Input topic or keywords?', validators=[Required()])
    submit = SubmitField('Submit')

class RefreshForm(Form):
    submit = SubmitField('Refresh')

@app.route('/', methods=['GET', 'POST'])
def index():
    form = KeywordsForm()
    fresh = RefreshForm()
    if form.validate_on_submit():
        # click event
        keyword = form.keywords.data

        keyword.replace(" ", "+")

        SEED_URL = "http://www.imdb.com/find?q="
        TITLE_SEARCH = "&s=tt&ttype=ft&ref_=fn_tt_pop"

        SEED_URL = SEED_URL + keyword + TITLE_SEARCH
        bfsSearchMovie(SEED_URL)


        old_name = session.get('keywords')
        if old_name is not None and old_name != form.keywords.data:
            flash('Looks like you have changed your keywords!')
        session['keywords'] = form.keywords.data
        return redirect(url_for('index'))

    if fresh.validate_on_submit():

        # Refresh entries
        entries.append()
        return redirect(url_for('index'))


    return render_template('index.html', form=form, name=session.get('keywords'),current_time=datetime.utcnow(), entries = [], fresh = fresh)


@app.errorhandler(404)
def page_not_found(e):
    return render_template('404.html'), 404


@app.errorhandler(500)
def internal_server_error(e):
    return render_template('500.html'), 500

if __name__ == '__main__':
    app.run(debug=True);
